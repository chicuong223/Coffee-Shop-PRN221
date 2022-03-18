﻿using System;
using System.Collections.Generic;

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
        public DateTime? BillDate { get; set; }
        public string StaffUsername { get; set; }
        public double? Discount { get; set; }
        public string VoucherId { get; set; }
        public bool? Status { get; set; }

        public virtual Staff StaffUsernameNavigation { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual IList<BillDetail> BillDetails { get; set; }
    }
}
