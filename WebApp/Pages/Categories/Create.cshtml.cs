using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.RepositoryInterface;

namespace WebApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IBaseRepository<Category> _context;

        public CreateModel(IBaseRepository<Category> context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.Status = true;

            //_context.Categories.Add(Category);
            //await _context.SaveChangesAsync();
            await _context.Create(Category);

            return RedirectToPage("./Index");
        }
    }
}
