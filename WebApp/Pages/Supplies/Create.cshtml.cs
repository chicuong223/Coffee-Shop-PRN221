using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;

namespace WebApp.Pages.Supplies
{
    public class CreateModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public CreateModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Supply Supply { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Supplies.Add(Supply);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
