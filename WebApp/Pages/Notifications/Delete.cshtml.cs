using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Notifications
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Notification Notification { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notifications.GetByID(id);

            if (Notification == null)
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

            Notification = await _context.Notifications.GetByID(id, false);

            if (Notification != null)
            {
                await _context.Notifications.Delete(Notification);
            }

            return RedirectToPage("./Index");
        }
    }
}
