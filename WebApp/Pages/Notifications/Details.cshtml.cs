﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Notifications
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DetailsModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public Notification Notification { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notifications
                .Include(n => n.SenderNavigation).FirstOrDefaultAsync(m => m.Id == id);

            if (Notification == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
