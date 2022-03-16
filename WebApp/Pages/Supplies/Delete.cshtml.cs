using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Supplies
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DeleteModel(WebApp.Models.CoffeeShopDBContext context)
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

            Supply = await _context.Supplies
                .Include(s => s.Product)
                .Include(s => s.Supplier).FirstOrDefaultAsync(m => m.ProductId == id);

            if (Supply == null)
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

            Supply = await _context.Supplies.FindAsync(id);

            if (Supply != null)
            {
                _context.Supplies.Remove(Supply);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
