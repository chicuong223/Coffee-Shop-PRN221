using System;
using System.Collections.Generic;

#nullable disable

namespace DataObject.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Bills = new HashSet<Bill>();
            Notifications = new HashSet<Notification>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
