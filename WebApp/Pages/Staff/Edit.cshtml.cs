using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly DataObject.Models.CoffeeShopDBContext _context;

        public EditModel(DataObject.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DataObject.Models.Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staff.FirstOrDefaultAsync(m => m.Username == id);

            if (Staff == null)
            {
                return NotFound();
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

            _context.Attach(Staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(Staff.Username))
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

        private bool StaffExists(string id)
        {
            return _context.Staff.Any(e => e.Username == id);
        }
    }
}
