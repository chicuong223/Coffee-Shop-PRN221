using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repository;
using X.PagedList;

namespace WebApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _context;

        public IndexModel(ICategoryRepository context)
        {
            _context = context;
        }

        public IPagedList<Category> Categories { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Categories = await _context.GetCategories(pageIndex);
        }
    }
}
