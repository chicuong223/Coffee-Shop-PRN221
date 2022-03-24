using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace WebApp.Pages
{
    public class StockModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRepoWrapper _context;
        public IPagedList<Product> Products { get; set; }
        public IEnumerable<NotificationDetail> NotiList { get; set; }
        public StockModel(ILogger<IndexModel> logger, IRepoWrapper _context)
        {
            this._context = _context;
            this._logger = logger;
        }
        public Category category { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex, int? categoryid)
        {

            ISession session = HttpContext.Session;
            var role = session.GetString("Role");
            var username = session.GetString("Username");
            if (role != null && role.Equals("Admin"))
            {
                return RedirectToPage("/Error");
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
            NotiList = await _context.NotificationDetails.GetAll(noti => noti.Notification.Sender.Equals(username), true);
            return Page();
        }
    }
}
