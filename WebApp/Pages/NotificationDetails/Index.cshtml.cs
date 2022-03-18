 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.NotificationDetails
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public IndexModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<NotificationDetail> NotificationDetail { get;set; }

        public async Task OnGetAsync()
        {
            NotificationDetail = await _context.NotificationDetails
                .Include(n => n.Notification)
                .Include(n => n.Product).ToListAsync();
        }
    }
}
