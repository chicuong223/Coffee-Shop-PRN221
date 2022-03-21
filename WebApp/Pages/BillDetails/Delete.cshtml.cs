using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.BillDetails
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public BillDetail BillDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? billid, int? productid)
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
            if (billid == null || productid == null)
            {
                return NotFound();
            }

            BillDetail = await _context.BillDetails.GetByID((billid.Value, productid.Value));

            if (BillDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? billid, int? productid)
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
            if (billid == null || productid == null)
            {
                return NotFound();
            }

            BillDetail = await _context.BillDetails.GetByID((billid.Value, productid.Value), false);

            if (BillDetail != null)
            {
                //restock the product
                var product = await _context.Products.GetByID(BillDetail.ProductId);
                product.Stock += BillDetail.Quantity;
                await _context.Products.Update(product);

                //delete the bill detail
                await _context.BillDetails.Delete((billid.Value, productid.Value));
            }

            return RedirectToPage("../Bills/Edit", new { id = BillDetail.BillId });
        }
    }
}
