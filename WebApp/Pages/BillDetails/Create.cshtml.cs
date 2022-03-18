using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.BillDetails
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
        ViewData["BillId"] = new SelectList(_context.Bills, "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public BillDetail BillDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BillDetails.Add(BillDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
