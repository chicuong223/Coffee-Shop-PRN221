﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.NotificationDetails
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public IndexModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<NotificationDetail> NotificationDetail { get;set; }

        public async Task OnGetAsync()
        {
            NotificationDetail = await _context.NotificationDetails
                .Include(n => n.Notification)
                .Include(n => n.Product).ToListAsync();
        }
    }
}
