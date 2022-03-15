using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool? Status { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
