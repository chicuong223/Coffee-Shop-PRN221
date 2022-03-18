using System;
using System.Collections.Generic;

#nullable disable

namespace DataObject.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
        }

        public int Id { get; set; }
        public string Sender { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? NotificationDate { get; set; }

        public virtual Staff SenderNavigation { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
    }
}
