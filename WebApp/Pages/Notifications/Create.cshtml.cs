using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataAccess.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Sender"] = new SelectList(_context.Staffs.GetAll(null).Result.ToList(), "Username", "Username");
            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.Notifications.Create(Notification);

            return RedirectToPage("./Index");
        }
    }
}
