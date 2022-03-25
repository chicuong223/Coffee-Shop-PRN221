using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class BillDetail
    {
        public int BillId { get; set; }
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum 1 unit")]
        public int? Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }
        [Display(Name = "Total")]
        public decimal? SubTotal { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
