using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Bills
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
            //ViewData["VoucherId"] = new SelectList(await _context.Vouchers.GetAll(null), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bill Bill { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if(string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if(!role.Equals("Staff"))
            {
                return RedirectToPage("../Error");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(!string.IsNullOrWhiteSpace(Bill.VoucherId))
            {
                var voucher = await _context.Vouchers.GetByID(Bill.VoucherId);
                if (voucher == null)
                {
                    ViewData["VoucherNotFound"] = "Voucher not found";
                    return Page();
                }
                if (voucher.Status == false)
                {
                    ViewData["VoucherNotFound"] = "Voucher is not valid!";
                    return Page();
                }
                if (voucher.UsageCount <= 0)
                {
                    ViewData["VoucherNotFound"] = "Voucher is not usable!";
                    return Page();
                }
                if (voucher.ExpirationDate.HasValue && voucher.ExpirationDate.Value.Date < DateTime.Now.Date)
                {
                    ViewData["VoucherNotFound"] = "Voucher is expired!";
                    return Page();
                }
            }
            
            Bill.Status = true;
            Bill.StaffUsername = currentUsername;
            Bill.BillDate = DateTime.Now;

            await _context.Bills.Create(Bill);

            return RedirectToPage("./Index");
        }
    }
}
