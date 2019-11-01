using System;
using System.Collections.Generic;

namespace UserRegistrationAPI.Entities
{
    public partial class Users
    {
        public Users()
        {
            Documents = new HashSet<Documents>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Documents> Documents { get; set; }
    }
}
