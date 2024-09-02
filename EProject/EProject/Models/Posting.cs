using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Posting
    {
        public Posting()
        {
            Exhibitions = new HashSet<Exhibition>();
        }

        public int PostId { get; set; }
        public string? PostImg { get; set; }
        public string? PostDate { get; set; }
        public int? PostUserId { get; set; }
        public int? PostCompetitionId { get; set; }

        public virtual Competition? PostCompetition { get; set; }
        public virtual UserRecord? PostUser { get; set; }
        public virtual ICollection<Exhibition> Exhibitions { get; set; }
    }
}
