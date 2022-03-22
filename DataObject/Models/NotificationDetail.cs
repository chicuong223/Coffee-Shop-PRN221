using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class NotificationDetail
    {
        public int NotificationId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum 1 unit")]
        public int Quantity { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual Product Product { get; set; }
    }
}
