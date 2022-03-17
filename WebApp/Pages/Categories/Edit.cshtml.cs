using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;

namespace WebApp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IBaseRepository<Category> _context;

        public EditModel(IBaseRepository<Category> context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Category).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                var category = await _context.GetByID(Category.Id, false);
                category.CategoryName = Category.CategoryName;
                await _context.Update(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
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

        private bool CategoryExists(int id)
        {
            //return _context.Categories.Any(e => e.Id == id);
            return _context.GetByID(id, false) != null;
        }
    }
}
