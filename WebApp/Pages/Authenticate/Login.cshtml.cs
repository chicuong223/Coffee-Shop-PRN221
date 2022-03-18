using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.RepositoryInterface;
using WebApp.Utilities;

namespace WebApp.Pages.Authenticate
{
    public class LoginModel : PageModel
    {
        private readonly CoffeeShopDBContext _dbContext;
        private readonly IBaseRepository<Staff> _staffRepository;

        public LoginModel(CoffeeShopDBContext context, IBaseRepository<Staff> staffRepository)
        {
            _dbContext = context;
            _staffRepository = staffRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            var admin = _dbContext.Admin();
            string hashedPassword = PasswordUtility.HashPassword(password);
            System.Console.WriteLine(admin.Password);
            System.Console.WriteLine(hashedPassword);
            ISession session = HttpContext.Session; 
            if(username.Equals(admin.Username) && hashedPassword.Equals(admin.Password))
            {
                session.SetString("Username", admin.Username);
                session.SetString("Role", "Admin");
                return RedirectToPage("../Index");
            }
            var staff = await _staffRepository.GetSingle(s => s.Username.Equals(username) && s.Password.Equals(hashedPassword));
            if(staff == null)
            {
                ViewData["BadCredentials"] = "Incorrect Username or Password";
                return Page();
            }
            session.SetString("Username", staff.Username);
            session.SetString("Role", "Staff");
            return RedirectToPage("../Index");
        }
    }
}