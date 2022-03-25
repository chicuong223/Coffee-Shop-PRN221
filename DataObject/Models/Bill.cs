using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataObject.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new List<BillDetail>();
        }

        public int Id { get; set; }
        [Display(Name = "Date")]
        public DateTime? BillDate { get; set; }
        [Display(Name = "Staff")]
        public string StaffUsername { get; set; }
        public double? Discount { get; set; }
        [Display(Name = "Voucher code")]
        public string VoucherId { get; set; }
        public bool? Status { get; set; }

        [Display(Name = "Staff")]
        public virtual Staff StaffUsernameNavigation { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual IList<BillDetail> BillDetails { get; set; }
    }
}
