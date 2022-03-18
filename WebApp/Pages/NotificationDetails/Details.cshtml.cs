using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.NotificationDetails
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DetailsModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public NotificationDetail NotificationDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NotificationDetail = await _context.NotificationDetails
                .Include(n => n.Notification)
                .Include(n => n.Product).FirstOrDefaultAsync(m => m.NotificationId == id);

            if (NotificationDetail == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
