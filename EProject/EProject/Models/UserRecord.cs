using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class UserRecord
    {
        public UserRecord()
        {
            Postings = new HashSet<Posting>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public long UserContactNum { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public int? UserStatus { get; set; }
        public int? UserRoleId { get; set; }

        public virtual Role? UserRole { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}
