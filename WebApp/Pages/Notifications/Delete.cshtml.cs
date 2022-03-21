using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;

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
            if (!isAdmin())
            {
                return RedirectToPage("./Index");
            }

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
            if(!isAdmin())
            {
                return RedirectToPage("./Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notifications.GetByID(id, false);

            if (Notification != null)
            {
                var details = await _context.NotificationDetails.GetAll(d => d.NotificationId == Notification.Id);
                foreach(var detail in details)
                {
                    await _context.NotificationDetails.Delete((detail.NotificationId, detail.ProductId));
                }
                await _context.Notifications.Delete(Notification.Id);
            }

            return RedirectToPage("./Index");
        }

        private bool isAdmin()
        {
            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            var role = session.GetString("Role");
            return (!string.IsNullOrWhiteSpace(role) && role.Equals("Admin"));
        }
    }
}
