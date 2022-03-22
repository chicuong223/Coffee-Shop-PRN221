using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRepoWrapper _context;
        public IPagedList<Product> Products { get; set; }
        public Bill ActiveBill { get; set; }
        public IEnumerable<BillDetail> BillDetails { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IRepoWrapper _context)
        {
            this._context = _context;
            this._logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            Products = await _context.Products.GetList(null, true, pageIndex);

            ActiveBill = await _context.Bills.GetSingle(b => b.Status.Value == false);
            if (ActiveBill != null)
            {
                BillDetails = await _context.BillDetails.GetAll(b => b.BillId == ActiveBill.Id, true);
            }
            return Page();
        }
    }
}
