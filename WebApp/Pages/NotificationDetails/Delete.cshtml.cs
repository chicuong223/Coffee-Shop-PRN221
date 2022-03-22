using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.NotificationDetails
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public NotificationDetail NotificationDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? notiId, int? productId)
        {
            if (notiId == null || productId == null)
            {
                return NotFound();
            }

            NotificationDetail = await _context.NotificationDetails.GetByID((notiId.Value, productId.Value), true);

            if (NotificationDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? notiId, int? productId)
        {
            if (notiId == null || productId == null)
            {
                return NotFound();
            }

            NotificationDetail = await _context.NotificationDetails.GetByID((notiId.Value, productId.Value), false);

            if (NotificationDetail != null)
            {
                await _context.NotificationDetails.Delete((NotificationDetail.NotificationId, NotificationDetail.ProductId));
            }
            return RedirectToPage("../Notifications/Create", new { id = NotificationDetail.NotificationId });
        }
    }
}
