using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Http;
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
        public Voucher voucher { get; set; }
        [BindProperty]
        public IEnumerable<BillDetail> BillDetails { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IRepoWrapper _context)
        {
            this._context = _context;
            this._logger = logger;
        }
        public Category category { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex, int? categoryid)
        {
            ISession session = HttpContext.Session;
            var role = session.GetString("Role");
            if(role != null && role.Equals("Admin"))
			{
                return RedirectToPage("/Error");
			}
            if(!string.IsNullOrEmpty(this.searchString))
            {
                Products = await _context.Products.GetList(p => p.ProductName.ToLower().Contains(searchString.ToLower()));
            }
            else if(categoryid != null)
			{
                category = await _context.Categories.GetByID(categoryid);
                Products = await _context.Products.GetList(p => p.CategoryId == category.Id, true, pageIndex);
                ViewData["active-cat"] = category.Id;
            }
            else
			{
                Products = await _context.Products.GetList(null, true, pageIndex);
            }
            ActiveBill = await _context.Bills.GetSingle(b => b.Status.Value == false);
            if (ActiveBill != null)
            {
                BillDetails = await _context.BillDetails.GetAll(b => b.BillId == ActiveBill.Id, true);
                if(ActiveBill.VoucherId != null)
				{
                    voucher = await _context.Vouchers.GetByID(ActiveBill.VoucherId);
				}
            }
            return Page();
        }

        public async Task<IActionResult> OnGetUpdateOrder()
		{
            foreach(var item in BillDetails)
			{
                Console.WriteLine(item.Quantity);
			}
            return Page();
		}
    }
}
