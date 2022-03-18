using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.Supplies
{
    public class DetailsModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public DetailsModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public Supply Supply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supply = await _context.Supplies
                .Include(s => s.Product)
                .Include(s => s.Supplier).FirstOrDefaultAsync(m => m.ProductId == id);

            if (Supply == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
