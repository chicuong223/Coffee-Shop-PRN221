using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Notifications
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DeleteModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notifications.FindAsync(id);

            if (Notification != null)
            {
                _context.Notifications.Remove(Notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
