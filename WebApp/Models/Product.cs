using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            NotificationDetails = new HashSet<NotificationDetail>();
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
        public bool? Status { get; set; }
        public string ImageURL { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
