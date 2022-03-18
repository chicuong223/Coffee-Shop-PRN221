using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.BillDetails
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public EditModel(IRepoWrapper context)
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
            ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
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
                return Page();
            }

            try
            {
                var product = await _context.Products.GetByID(BillDetail.ProductId);
                if (product == null)
                {
                    return RedirectToPage("../Error");
                }
                BillDetail.SubTotal = product.Price * BillDetail.Quantity;
                BillDetail.UnitPrice = product.Price;
                await _context.BillDetails.Update(BillDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return RedirectToPage("../Bills/Edit", new { id = BillDetail.BillId });
        }
    }
}
