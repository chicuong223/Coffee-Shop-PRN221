using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Pages.Supplies
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public EditModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Supply Supply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supply = await _context.Supplies.GetByID(id, false);

            if (Supply == null)
            {
                return NotFound();
            }
           ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
           ViewData["SupplierId"] = new SelectList(_context.Suppliers.GetAll(null).Result.ToList(), "Id", "Id");
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


            try
            {
                await _context.Supplies.Update(Supply);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(Supply.ProductId))
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

        private bool SupplyExists(int id)
        {
            return _context.Supplies.GetByID(id, false)!=null;
        }
    }
}
