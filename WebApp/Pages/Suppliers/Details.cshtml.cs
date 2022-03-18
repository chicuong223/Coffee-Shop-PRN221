using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Pages.Suppliers
{
    public class DetailsModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DetailsModel(IRepoWrapper context)
        {
            _context = context;
        }

        public Supplier Supplier { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier = await _context.Suppliers.GetByID(id);

            if (Supplier == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
