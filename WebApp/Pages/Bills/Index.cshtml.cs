using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;
using X.PagedList;

namespace WebApp.Pages.Bills
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IPagedList<Bill> Bills { get;set; }

        public async Task OnGetAsync(int? page)
        {
            //Bill = await _context.Bills
            //    .Include(b => b.StaffUsernameNavigation)
            //    .Include(b => b.Voucher).ToListAsync();
            Bills = await _context.Bills.GetList(null, true, page);
        }
    }
}
