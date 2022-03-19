using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Supplies
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            Product = await _context.Products.GetByID(ProductId);
            if (Product == null)
            {
                return NotFound();
            }
        //ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.GetAll(null).Result.ToList(), "Id", "Name");
            Now = DateTime.Now;
            return Page();
        }
        [BindProperty]
        public DateTime Now { get; set; }
        public Product Product { get; set; }
        [BindProperty]
        public Supply Supply { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", new { ProductId = Product.Id });
            }
            Supply.SupplyDate = DateTime.Now;
            await _context.Supplies.Create(Supply);

            return RedirectToPage("./Index");
        }
    }
}
