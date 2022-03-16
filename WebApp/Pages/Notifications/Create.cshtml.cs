using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Pages.Notifications
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
        ViewData["Sender"] = new SelectList(_context.Staff, "Username", "Username");
            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
