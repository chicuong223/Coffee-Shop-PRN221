using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Bills
{
    public class DetailsModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DetailsModel(IRepoWrapper context)
        {
            _context = context;
        }

        public Bill Bill { get; set; }

        public decimal Total { get; set; }

        public IList<BillDetail> Details { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bill = await _context.Bills.GetByID(id);

            if (Bill == null)
            {
                return NotFound();
            }

            Details = (await _context.BillDetails.GetAll(detail => detail.BillId == id, true)).ToList();

            Total = 0;
            foreach(var detail in Details)
            {
                Total += detail.SubTotal.Value;
            }
            if(Bill.Voucher != null)
            {
                Total *= 1 - (decimal)(Bill.Voucher.Percentage.Value / 100);
            }

            return Page();
        }
    }
}
