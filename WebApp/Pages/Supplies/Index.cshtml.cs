using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace DataAccess.Pages.Supplies
{
    public class IndexModel : PageModel
    {
        private readonly CoffeeShopDBContext _context;

        public IndexModel(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public IList<Supply> Supply { get;set; }

        public async Task OnGetAsync()
        {
            Supply = await _context.Supplies
                .Include(s => s.Product)
                .Include(s => s.Supplier).ToListAsync();
        }
    }
}
