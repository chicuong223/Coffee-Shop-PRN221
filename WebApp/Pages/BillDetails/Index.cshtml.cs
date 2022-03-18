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

namespace DataAccess.Pages.BillDetails
{
    public class IndexModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public IndexModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IPagedList<BillDetail> BillDetail { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            BillDetail = await _context.BillDetails.GetList(null, null, pageIndex);
        }
    }
}
