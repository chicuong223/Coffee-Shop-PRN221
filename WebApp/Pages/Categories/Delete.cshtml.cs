using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;

namespace WebApp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IBaseRepository<Category> _context;

        public DeleteModel(IBaseRepository<Category> context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            Category = await _context.GetByID(id, false);

            if (Category == null)
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

            //Category = await _context.Categories.FindAsync(id);
            Category = await _context.GetByID(id, false);

            if (Category != null)
            {
                //_context.Categories.Remove(Category);
                //await _context.SaveChangesAsync();
                await _context.Delete(Category.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
