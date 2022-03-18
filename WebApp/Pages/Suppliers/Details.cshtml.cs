using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Suppliers
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DetailsModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public Supplier Supplier { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier = await _context.Suppliers.FirstOrDefaultAsync(m => m.Id == id);

            if (Supplier == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
