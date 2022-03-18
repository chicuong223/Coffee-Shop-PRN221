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

namespace WebApp.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IPagedList<DataObject.Models.Staff> Staff { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Staff = await _context.Staffs.GetList(null, false, pageIndex);
        }
    }
}
