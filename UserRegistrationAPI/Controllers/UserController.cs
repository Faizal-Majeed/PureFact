using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserRegistrationAPI.Entities;
using UserRegistrationAPI.Helpers;
using UserRegistrationAPI.Models.Users;
using UserRegistrationAPI.Services;

namespace UserRegistrationAPI.Controllers
{
   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private const int TokenExpiryTime = 4;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;

        private readonly ILogger<UserController> _logger;
        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings, IMemoryCache memoryCache, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _cache = memoryCache;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel.Email, loginModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(TokenExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            _cache.Set($"Bearer {tokenString}", "userToken", TimeSpan.FromHours(TokenExpiryTime));

            return Ok(new
            {
                Id = user.UserId,
                Username = user.Email,
                FirstName = user.FirstName,
                LastName = user.Lastname,
                Token = tokenString
            });
        }

        [HttpPost("logout")]
        public IActionResult LogOut (){
            var request = HttpContext.Request;
            Microsoft.Extensions.Primitives.StringValues value;
            var token = request.Headers.TryGetValue("Authorization",out value);
             _cache.Remove(value[0]);
             return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]NewUserModel model)
        {
            var user = _mapper.Map<Users>(model);
            try
            {
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (ExceptionHandler ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateModel model)
        {
            var user = _mapper.Map<Users>(model);
            user.UserId = id;

            try
            {
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (ExceptionHandler ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }

     
}