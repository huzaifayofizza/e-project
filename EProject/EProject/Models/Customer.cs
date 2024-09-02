using System;
using System.Collections.Generic;

namespace EProject.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public long CustomerContactInfo { get; set; }
    }
}
