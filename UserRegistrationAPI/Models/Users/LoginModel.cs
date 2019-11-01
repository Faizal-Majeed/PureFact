using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.Users
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}