using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using WebApp.Utilities;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Authenticate
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "This field cannot be empty")]
        public string username { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "This field cannot be empty")]
        public string password { get; set; }
        private readonly CoffeeShopDBContext _dbContext;
        private readonly IRepoWrapper _context;

        public LoginModel(CoffeeShopDBContext context, IRepoWrapper staffRepository)
        {
            _dbContext = context;
            _context = staffRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            var admin = _dbContext.Admin();
            string hashedPassword = PasswordUtility.HashPassword(password);
            ISession session = HttpContext.Session;
            if (username.Equals(admin.Username) && hashedPassword.Equals(admin.Password))
            {
                session.SetString("Username", admin.Username);
                session.SetString("Role", "Admin");
                return RedirectToPage("../Index");
            }
            var staff = await _context.Staffs.GetSingle(s => s.Username.Equals(username) && s.Password.Equals(hashedPassword));

            if (staff == null)
            {
                ViewData["BadCredentials"] = "Incorrect Username or Password";
                return Page();
            }
            session.SetString("Username", staff.Username);
            session.SetString("Role", "Staff");
            return RedirectToPage("../Index");
        }

        public IActionResult OnPostLogout()
        {
            ISession session = HttpContext.Session;
            session.Clear();
            return Page();
        }
    }
}
