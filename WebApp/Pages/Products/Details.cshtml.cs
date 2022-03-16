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
    public class DetailsModel : PageModel
    {
        private readonly IBaseRepository<Product> _context;

        public DetailsModel(IBaseRepository<Product> context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.GetByID(id, true);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
