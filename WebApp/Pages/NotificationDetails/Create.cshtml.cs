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

        public async Task<IActionResult> OnGetAsync(int? notiid)
        {
            if(notiid == null)
            {
                return NotFound();
            }
            var notification = await _context.Notifications.GetByID(notiid, false);
            if(notification == null)
            {
                return NotFound();
            }
            NotificationDetail = new NotificationDetail();
            NotificationDetail.NotificationId = notification.Id;

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
                return RedirectToPage("./Create", new {notiid = NotificationDetail.NotificationId});
            }

            var detail = await _context.NotificationDetails.GetByID((NotificationDetail.NotificationId, NotificationDetail.ProductId));
            if (detail != null)
            {
                detail.Quantity = NotificationDetail.Quantity;
                await _context.NotificationDetails.Update(detail);
            }
            else
            {
                await _context.NotificationDetails.Create(NotificationDetail);
            }

            return RedirectToPage("../Notifications/Create", new {id = NotificationDetail.NotificationId});
        }
    }
}
