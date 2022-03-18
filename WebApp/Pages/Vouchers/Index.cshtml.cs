﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.Vouchers
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public IndexModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<Voucher> Voucher { get;set; }

        public async Task OnGetAsync()
        {
            Voucher = await _context.Vouchers.ToListAsync();
        }
    }
}
