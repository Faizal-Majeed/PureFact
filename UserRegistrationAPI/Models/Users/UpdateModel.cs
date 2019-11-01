using System;

namespace UserRegistrationAPI.Models.Users
{
  public class UpdateModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}