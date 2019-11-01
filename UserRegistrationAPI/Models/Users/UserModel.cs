using System;

namespace UserRegistrationAPI.Models.Users
{
  public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}