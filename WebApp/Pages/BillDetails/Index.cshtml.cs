﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.BillDetails
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public IndexModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<BillDetail> BillDetail { get;set; }

        public async Task OnGetAsync()
        {
            BillDetail = await _context.BillDetails
                .Include(b => b.Bill)
                .Include(b => b.Product).ToListAsync();
        }
    }
}
