using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;

namespace WebApp.Pages.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly DataObject.Models.CoffeeShopDBContext _context;

        public DetailsModel(DataObject.Models.CoffeeShopDBContext context)
        {
            _context = context;
        }

        public DataObject.Models.Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staff.FirstOrDefaultAsync(m => m.Username == id);

            if (Staff == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
