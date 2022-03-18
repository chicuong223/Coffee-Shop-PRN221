using System;
using System.Collections.Generic;

#nullable disable

namespace DataObject.Models
{
    public partial class NotificationDetail
    {
        public int NotificationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual Product Product { get; set; }
    }
}
