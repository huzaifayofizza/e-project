using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Award
    {
        public int AwardId { get; set; }
        public int? AwardUserId { get; set; }
        public int? AwardCompetitionId { get; set; }
        public string? AwardCatg { get; set; }
        public string? AwardRemarks { get; set; }

        public virtual Competition? AwardCompetition { get; set; }
        public virtual User? AwardUser { get; set; }
    }
}
