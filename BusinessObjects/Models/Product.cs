using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
        public bool? Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
