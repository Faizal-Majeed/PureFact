using System;
using System.Collections.Generic;

namespace UserRegistrationAPI.Entities
{
    public partial class Documents
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
