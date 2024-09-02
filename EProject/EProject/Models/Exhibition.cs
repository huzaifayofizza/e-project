using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Exhibition
    {
        public int ExhibitionId { get; set; }
        public string? ExhibitionImage { get; set; }
        public string? ExhibitionPrice { get; set; }
        public int? ExhibitionPostingId { get; set; }

        public virtual Posting? ExhibitionPosting { get; set; }
    }
}
