using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Notifications
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public EditModel(IRepoWrapper context)
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

            if(Notification.IsSent)
            {
                TempData["NotificationError"] = "This notification is already sent";
                return RedirectToPage("./Details", new { id = Notification.Id });
            }

            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            if (username != null)
            {
                if(!username.Equals(Notification.Sender))
                {
                    TempData["NotificationError"] = "You cannot modify this notification";
                    return RedirectToPage("./Index");
                }
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

            if (Notification.IsSent)
            {
                TempData["NotificationError"] = "This notification is already sent";
                return RedirectToPage("./Details", new { id = Notification.Id });
            }

            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            if (username != null)
            {
                if (!username.Equals(Notification.Sender))
                {
                    TempData["NotificationError"] = "You cannot modify this notification";
                    return RedirectToPage("./Index");
                }
            }

            try
            {
                await _context.Notifications.Update(Notification);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(Notification.Id))
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

        private bool NotificationExists(int id)
        {
            return _context.Notifications.GetByID(id, false)!=null;
        }
    }
}
