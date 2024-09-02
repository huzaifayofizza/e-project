using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class ExhibitionPrice
    {
        public int ExhibitionPriceId { get; set; }
        public long ExhibitionPriceAmount { get; set; }
        public int? ExhibitionId { get; set; }

        public virtual Exhibition? Exhibition { get; set; }
    }
}
