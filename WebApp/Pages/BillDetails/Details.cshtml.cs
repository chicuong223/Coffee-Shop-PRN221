using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Pages.BillDetails
{
    public class DetailsModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DetailsModel(IRepoWrapper context)
        {
            _context = context;
        }

        public BillDetail BillDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BillDetail = await _context.BillDetails.GetByID(id, false);

            if (BillDetail == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
