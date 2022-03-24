using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using X.PagedList;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IPagedList<Product> Products { get;set; }
        

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            ISession session = HttpContext.Session;
            var role = session.GetString("Role");
            if (!string.IsNullOrEmpty(role) && !role.Equals("Admin")) {
                return RedirectToPage("../Unauthorized");
            }
            Products = await _context.Products.GetList(null, true, pageIndex);
            return Page();
        }
    }
}
