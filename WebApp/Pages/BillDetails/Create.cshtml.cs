using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataAccess.Pages.BillDetails
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BillId"] = new SelectList(_context.Bills.GetAll(null).Result.ToList(), "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
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

            await _context.BillDetails.Create(BillDetail);

            return RedirectToPage("./Index");
        }
    }
}
