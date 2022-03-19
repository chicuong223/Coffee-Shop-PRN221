using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using X.PagedList;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Vouchers
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IPagedList<Voucher> Voucher { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Voucher = await _context.Vouchers.GetList(null, null, pageIndex);
        }
    }
}
