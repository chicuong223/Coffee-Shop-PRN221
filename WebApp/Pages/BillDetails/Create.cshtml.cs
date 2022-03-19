using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using DataObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.BillDetails
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet([FromQuery(Name = "billid")] int? billId)
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Staff"))
            {
                return RedirectToPage("../Error");
            }
            if (billId == null)
            {
                return RedirectToPage("../Error");
            }
            var billExists = await _context.Bills.GetByID(billId.Value) != null;
            if (!billExists)
            {
                return RedirectToPage("../Bills/Index");
            }
            BillID = billId.Value;
            ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public BillDetail BillDetail { get; set; }

        public int BillID { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? billId)
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Staff"))
            {
                return RedirectToPage("../Error");
            }
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all fields";
                return RedirectToPage("./Create", new { billId = billId.Value });
            }

            var product = await _context.Products.GetByID(BillDetail.ProductId);
            if (product != null)
            {
                var detail = await _context.BillDetails.GetByID((billId.Value, product.Id), false);
                if(detail != null)
                {
                    TempData["Error"] = "Product is already in this bill";
                    return RedirectToPage("./Create", new { billId = billId.Value });
                }
                BillDetail.UnitPrice = product.Price;
                BillDetail.SubTotal = product.Price * BillDetail.Quantity;
            }
            BillDetail.BillId = billId.Value;

            await _context.BillDetails.Create(BillDetail);

            return RedirectToPage("../Bills/Details", new { id = BillDetail.BillId });
        }
    }
}
