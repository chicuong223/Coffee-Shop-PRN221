using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryRepository _context;

        public DetailsModel(ICategoryRepository context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.GetByID(id.Value);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
