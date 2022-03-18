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
using WebApp.Utilities;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(IRepoWrapper context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staffs.GetByID(id, false);

            if (Staff == null)
            {
                return NotFound();
            }

            bool allowed = false;
           

            //Check if user is admin
            if (role.Equals("Admin"))
            {
                allowed = true;
            }
            else
            {
                if (id.Equals(username))
                {
                    allowed = true;
                }
            }

            if (!allowed)
            {
                return RedirectToAction("../Error");
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile avatar)
        {
            bool allowed = false;
            ISession session = HttpContext.Session;
            var username = session.GetString("Username");
            var role = session.GetString("Role");
            if(username == null || role == null)
            {
                return RedirectToPage("../Authenticate/Login");
            }

            //Check if user is admin
            if(role.Equals("Admin"))
            {
                allowed = true;
            }
            else
            {
                if(Staff.Username.Equals(username))
                {
                    allowed = true;
                }
            }

            if(!allowed)
            {
                return RedirectToAction("../Error");
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if(avatar != null)
                {
                    await FileUtility.UploadFile(avatar, _env);
                    Staff.AvatarUrl = avatar.FileName;
                }
                await _context.Staffs.Update(Staff);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToPage("./Index");
        }

    }
}
