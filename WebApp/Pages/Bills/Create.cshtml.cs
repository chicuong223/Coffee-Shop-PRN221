using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Pages.Bills
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
        ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
        ViewData["VoucherId"] = new SelectList(_context.Vouchers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bill Bill { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bills.Add(Bill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
