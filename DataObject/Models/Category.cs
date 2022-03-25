using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Name")]
        public string CategoryName { get; set; }
        public bool Status { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
