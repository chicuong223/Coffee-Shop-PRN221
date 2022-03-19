using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.NotificationDetails
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
        ViewData["NotificationId"] = new SelectList(_context.Notifications.GetList(null).Result.ToList(), "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Products.GetList(null).Result.ToList(), "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public NotificationDetail NotificationDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.NotificationDetails.Create(NotificationDetail);

            return RedirectToPage("./Index");
        }
    }
}
