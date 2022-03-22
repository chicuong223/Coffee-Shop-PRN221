using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class BillDetail
    {
        public int BillId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum 1 unit")]
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
