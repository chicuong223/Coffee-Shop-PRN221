using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.Bills
{
    public class DetailsModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public DetailsModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

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
    }
}
