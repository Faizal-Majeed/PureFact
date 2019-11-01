using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationAPI.Entities;
using UserRegistrationAPI.Helpers;
using UserRegistrationAPI.Models.Documents;
using UserRegistrationAPI.Services;

namespace UserRegistrationAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController: ControllerBase
    {
         private IUserService _userService;
        private IMapper _mapper;

        public DocumentController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

         [HttpPost("{userId}")]
        public IActionResult AddDocument(int userId, [FromBody]DocumentModel model)
        {
            var documents = _mapper.Map<Documents>(model);
            documents.UserId = userId;
            try
            {
                _userService.AddDocument(documents);
                return Ok();
            }
            catch (ExceptionHandler ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetDocument(int userId)
        {
            var documents = _userService.GetDocuments(userId);
            var model = _mapper.Map<IEnumerable<DocumentModel>>(documents);
            return Ok(model);
        }
    }
}