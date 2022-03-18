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

namespace DataAccess.Pages.NotificationDetails
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public EditModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public NotificationDetail NotificationDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NotificationDetail = await _context.NotificationDetails.GetByID(id);

            if (NotificationDetail == null)
            {
                return NotFound();
            }
           ViewData["NotificationId"] = new SelectList(_context.Notifications.GetAll(null).Result.ToList(), "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
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


            try
            {
                await _context.NotificationDetails.Update(NotificationDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationDetailExists(NotificationDetail.NotificationId))
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

        private bool NotificationDetailExists(int id)
        {
            return _context.NotificationDetails.GetByID(id)!=null;
        }
    }
}
