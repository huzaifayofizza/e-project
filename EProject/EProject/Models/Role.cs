using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRecords = new HashSet<UserRecord>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserRecord> UserRecords { get; set; }
    }
}
