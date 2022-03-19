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

namespace WebApp.Pages.Staff
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public DataObject.Models.Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            var role = session.GetString("Role");
            if (username == null || role == null)
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if(!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }
           
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staffs.GetByID(id, false);

            if (Staff == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            var role = session.GetString("Role");
            if (username == null || role == null)
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staffs.GetByID(id, false);

            if (Staff != null)
            {
                //_context.Staff.Remove(Staff);
                //await _context.SaveChangesAsync();
                Staff.Status = false;
                await _context.Staffs.Update(Staff);
            }

            return RedirectToPage("./Index");
        }
    }
}
