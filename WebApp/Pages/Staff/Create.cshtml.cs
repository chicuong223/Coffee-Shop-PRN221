using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using WebApp.Utilities;

namespace WebApp.Pages.Staff
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataObject.Models.Staff Staff { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");

            if(string.IsNullOrEmpty(currentUsername)|| string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if(!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var exist = (await _context.Staffs.GetByID(Staff.Username, false)) != null;
            if(exist)
            {
                ViewData["UsernameError"] = "Username is already used!";
                return Page();
            }

            exist = (await _context.Staffs.GetSingle(u => u.Email.Equals(Staff.Email))) != null;
            if (exist)
            {
                ViewData["EmailError"] = "Email is already used!";
                return Page();
            }

            Staff.Password = PasswordUtility.HashPassword(Staff.Password);

            Staff.Status = true;

            await _context.Staffs.Create(Staff);

            return RedirectToPage("./Index");
        }
    }
}
