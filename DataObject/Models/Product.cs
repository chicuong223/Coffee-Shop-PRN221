﻿using System;
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

        [Display(Name = "Product ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be 2 to 100 character long")]
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range( 1, 1000000000,ErrorMessage = "Price must be positive")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        [Range( 1, 1000000000,ErrorMessage = "Stock must be positive")]
        public int? Stock { get; set; }
        public bool? Status { get; set; }
        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
