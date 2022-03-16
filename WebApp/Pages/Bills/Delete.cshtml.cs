using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Bills
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DeleteModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bill Bill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bill = await _context.Bills
                .Include(b => b.StaffUsernameNavigation)
                .Include(b => b.Voucher).FirstOrDefaultAsync(m => m.Id == id);

            if (Bill == null)
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

            Bill = await _context.Bills.FindAsync(id);

            if (Bill != null)
            {
                _context.Bills.Remove(Bill);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
