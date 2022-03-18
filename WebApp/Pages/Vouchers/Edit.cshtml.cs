using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.Vouchers
{
    public class EditModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public EditModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Voucher Voucher { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Voucher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(Voucher.Id))
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

        private bool VoucherExists(string id)
        {
            return _context.Vouchers.Any(e => e.Id == id);
        }
    }
}
