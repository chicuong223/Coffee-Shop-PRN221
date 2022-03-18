using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Models.CoffeeShopDBContext _context;

        public IndexModel(WebApp.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<Notification> Notification { get;set; }

        public async Task OnGetAsync()
        {
            Notification = await _context.Notifications
                .Include(n => n.SenderNavigation).ToListAsync();
        }
    }
}
