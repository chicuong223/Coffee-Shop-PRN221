using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.BillDetails
{
    public class DeleteModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public DeleteModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BillDetail BillDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BillDetail = await _context.BillDetails
                .Include(b => b.Bill)
                .Include(b => b.Product).FirstOrDefaultAsync(m => m.BillId == id);

            if (BillDetail == null)
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

            BillDetail = await _context.BillDetails.FindAsync(id);

            if (BillDetail != null)
            {
                _context.BillDetails.Remove(BillDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
