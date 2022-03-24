using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using System;
using System.Globalization;

namespace WebApp.Pages.Supplies
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Supply Supply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productid, int? supplierid, string supplydate)
        {
            //if (productid == null || supplierid == null || !supplydate.HasValue)
            //{
            //    return NotFound();
            //}
            DateTime time = DateTime.ParseExact(supplydate, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);

            Supply = await _context.Supplies.GetByID((supplierid.Value, productid.Value, time), true);

            if (Supply == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? productid, int? supplierid, string supplydate)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Supply = await _context.Supplies.GetByID(id, false);

            Console.WriteLine(productid);
            Console.WriteLine(supplierid);
            Console.WriteLine(supplydate);

            var time = DateTime.ParseExact(supplydate, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
            Supply = await _context.Supplies.GetByID((supplierid.Value, productid.Value, time));

            if (Supply != null)
            {
                await _context.Supplies.Delete((Supply.SupplierId, Supply.ProductId, Supply.SupplyDate.Value));
            }

            return RedirectToPage("./Index");
        }
    }
}
