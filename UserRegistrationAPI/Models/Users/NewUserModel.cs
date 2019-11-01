using System;
using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.Users
{
    public class NewUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}