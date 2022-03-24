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
using System.ComponentModel.DataAnnotations;

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

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            //Bill = await _context.Bills
            //    .Include(b => b.StaffUsernameNavigation)
            //    .Include(b => b.Voucher).ToListAsync();
            if (From != null && To != null)
            {
                Bills = await _context.Bills.GetList((b => b.BillDate >= From && b.BillDate <= To.Value.AddDays(1) && b.Status.Value == true), true, pageIndex);
            }
            else if (From != null)
            {
                Bills = await _context.Bills.GetList((b => b.BillDate >= From && b.Status.Value == true), true, pageIndex);
            }
            else if (To != null)
            {
                Bills = await _context.Bills.GetList((b => b.BillDate <= To.Value.AddDays(1) && b.Status.Value == true), true, pageIndex);
            }
            else
            {
                Bills = await _context.Bills.GetList(b => b.Status.Value == true, true, pageIndex);
            }
        }
        
    }
}
