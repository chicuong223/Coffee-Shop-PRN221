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

namespace WebApp.Pages.Bills
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public EditModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Bill Bill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Staff"))
            {
                return RedirectToPage("../Error");
            }
            if (id == null)
            {
                return NotFound();
            }

            Bill = await _context.Bills.GetByID(id);

            if (Bill == null)
            {
                return NotFound();
            }
            ModelState.Clear();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Staff"))
            {
                return RedirectToPage("../Error");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _context.Bills.Update(Bill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(Bill.Id))
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

        private bool BillExists(int id)
        {
            return _context.Bills.GetByID(id, false)!=null;
        }
    }
}
