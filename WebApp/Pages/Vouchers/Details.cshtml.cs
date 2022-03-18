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
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public DetailsModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

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
    }
}
