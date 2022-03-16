using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;

namespace WebApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IBaseRepository<Product> _context;

        public DeleteModel(IBaseRepository<Product> context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Product = await _context.Products
            //    .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            Product = await _context.GetByID(id);

            if (Product == null)
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

            //Product = await _context.Products.FindAsync(id);

            Product = await _context.GetByID(id.Value);

            if (Product != null)
            {
                if(!Product.Status.Value)
                {
                    TempData["Error"] = "This product is already inactive!";
                    return await OnGetAsync(Product.Id);
                }
                //_context.Products.Remove(Product);
                //await _context.SaveChangesAsync();
                await _context.Delete(Product.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
