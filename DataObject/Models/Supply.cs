using System;
using System.Collections.Generic;

#nullable disable

namespace DataObject.Models
{
    public partial class Supply
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? SupplyDate { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
