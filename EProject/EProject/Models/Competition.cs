using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Competition
    {
        public Competition()
        {
            Postings = new HashSet<Posting>();
        }

        public int CompetitionId { get; set; }
        public string? CompetitionTitle { get; set; }
        public string? CompetitionBanner { get; set; }
        public string? CompStartDate { get; set; }
        public string? CompEndDate { get; set; }

        public virtual ICollection<Posting> Postings { get; set; }
    }
}
