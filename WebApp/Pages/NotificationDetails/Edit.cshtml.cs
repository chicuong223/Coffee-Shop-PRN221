using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.NotificationDetails
{
    public class EditModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public EditModel(CoffeeShopDBContext context)
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
           ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NotificationDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationDetailExists(NotificationDetail.NotificationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotificationDetailExists(int id)
        {
            return _context.NotificationDetails.Any(e => e.NotificationId == id);
        }
    }
}
