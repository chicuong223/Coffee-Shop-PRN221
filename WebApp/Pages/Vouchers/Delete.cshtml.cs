using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Vouchers
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DeleteModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Voucher Voucher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Voucher = await _context.Vouchers.FirstOrDefaultAsync(m => m.Id == id);

            if (Voucher == null)
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

            Voucher = await _context.Vouchers.FindAsync(id);

            if (Voucher != null)
            {
                _context.Vouchers.Remove(Voucher);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
