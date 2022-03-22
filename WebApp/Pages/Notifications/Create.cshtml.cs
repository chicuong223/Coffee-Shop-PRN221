using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            //Get current User
            ISession session = HttpContext.Session;
            string username = session.GetString("Username");
            string role = session.GetString("Role");
            if (username == null || role == null)
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (role.Trim().Equals("Admin"))
            {
                return RedirectToPage("../Unauthorized");
            }

            if (id == null)
            {
                //create notifcation into database
                Notification = new Notification
                {
                    IsRead = false,
                    IsSent = false,
                    NotificationDate = DateTime.Now,
                    Sender = username
                };
                await _context.Notifications.Create(Notification);
            }
            else
            {
                Notification = await _context.Notifications.GetByID(id, false);
            }

            Details = (await _context.NotificationDetails.GetAll(detail => detail.NotificationId == Notification.Id, true)).ToList();

            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; }

        public IList<NotificationDetail> Details { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Details = (await _context.NotificationDetails.GetAll(d => d.NotificationId == Notification.Id)).ToList();
            if (Details.Count <= 0)
            {
                TempData["NotificationError"] = "Please add at least 1 detail";
                return RedirectToPage("./Create", new { id = Notification.Id });
            }

            //set sender to current user
            ISession session = HttpContext.Session;
            string username = session.GetString("Username");
            if(username != null)
            {
                Notification.Sender = username;
            }
            else
            {
                return RedirectToPage("../Authenticate/Login");
            }

            Notification.NotificationDate = DateTime.Now;
            Notification.IsSent = true;
            await _context.Notifications.Update(Notification);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetCancel(int? id)
        {
            if (id == null) return NotFound();
            var notification = await _context.Notifications.GetByID(id);
            if (notification == null) return NotFound();
            var details = await _context.NotificationDetails.GetAll(d => d.NotificationId == notification.Id);
            foreach (var detail in details)
            {
                await _context.NotificationDetails.Delete((detail.NotificationId, detail.ProductId));
            }
            await _context.Notifications.Delete(notification.Id);
            return RedirectToPage("./Index");
        }
    }
}
