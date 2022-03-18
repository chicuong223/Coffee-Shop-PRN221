using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
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
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be 2 to 100 character long")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        public int? Stock { get; set; }
        public bool? Status { get; set; }
        public string ImageURL { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
