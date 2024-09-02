using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class User
    {
        public User()
        {
            Awards = new HashSet<Award>();
            Postings = new HashSet<Posting>();
        }

        public int UserId { get; set; }
        public string? UserEmail { get; set; }

        public virtual ICollection<Award> Awards { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}
