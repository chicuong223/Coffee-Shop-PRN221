using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Pages.Vouchers
{
    public class DetailsModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DetailsModel(IRepoWrapper context)
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

            Voucher = await _context.Vouchers.GetByID(id);

            if (Voucher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
