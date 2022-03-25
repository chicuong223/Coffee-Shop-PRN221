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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public Product ProductUpdate { get; set; }

        public IPagedList<Product> Products { get;set; }
        public Category category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex, int? id, int? categoryid)
        {
            ISession session = HttpContext.Session;
            var role = session.GetString("Role");
            if (!string.IsNullOrEmpty(role) && !role.Equals("Admin")) {
                return RedirectToPage("../Unauthorized");
            }
            if (categoryid != null)
            {
                category = await _context.Categories.GetByID(categoryid);
                Products = await _context.Products.GetList(p => p.CategoryId == category.Id, true, pageIndex);
                ViewData["active-cat"] = category.Id;
            }
            else
            {
                Products = await _context.Products.GetList(null, true, pageIndex);
            }
            if (id != null)
			{
                ProductUpdate = await _context.Products.GetByID(id);
			}
            var categories = await _context.Categories.GetAll(ca => ca.Status == true);
            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return Page();
        }
    }
}
