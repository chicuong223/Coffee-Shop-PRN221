using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.NotificationDetails
{
    public class DeleteModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public DeleteModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NotificationDetail = await _context.NotificationDetails.FindAsync(id);

            if (NotificationDetail != null)
            {
                _context.NotificationDetails.Remove(NotificationDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
