using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public DateTime? BillDate { get; set; }
        public string StaffUsername { get; set; }
        public double? Discount { get; set; }
        public int? TableId { get; set; }
        public int? VoucherId { get; set; }
        public bool? Status { get; set; }

        public virtual staff StaffUsernameNavigation { get; set; }
        public virtual Table Table { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
