using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class PaintingPrice
    {
        public int PriceId { get; set; }
        public long PriceAmount { get; set; }
        public int? PricePostId { get; set; }

        public virtual Posting? PricePost { get; set; }
    }
}
