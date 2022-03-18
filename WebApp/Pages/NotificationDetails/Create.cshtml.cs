using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Pages.NotificationDetails
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public CreateModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public NotificationDetail NotificationDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NotificationDetails.Add(NotificationDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
