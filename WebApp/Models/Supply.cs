using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Supply
    {
        public int IngredientId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? SupplyDate { get; set; }
        public int? Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
