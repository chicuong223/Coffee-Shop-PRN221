﻿using System;
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

        public async Task<IActionResult> OnGetAsync()
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
                return RedirectToPage("../Unauthorized");
            }
            //ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
            //ViewData["VoucherId"] = new SelectList(await _context.Vouchers.GetAll(null), "Id", "Id");
            Bill bill = new Bill();
            bill.Status = false;
            bill.StaffUsername = currentUsername;
            bill.BillDate = DateTime.Now;
            await _context.Bills.Create(bill);

            return RedirectToPage("../Index");
        }
        public async Task<IActionResult> OnGetRemoveVoucher(int? billId)
        {
            if (billId == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.GetByID(billId.Value);
            if (bill == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(bill.VoucherId))
            {
                bill.VoucherId = null;
                bill.Discount = 0;
                await _context.Bills.Update(bill);
            }
            return RedirectToPage("../Index");
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
                return RedirectToPage("../Unauthorized");
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
            
            Bill.Status = false;
            Bill.StaffUsername = currentUsername;
            Bill.BillDate = DateTime.Now;

            await _context.Bills.Create(Bill);

            return RedirectToPage("../Index");
        }
        public async Task<IActionResult> OnPostApplyVoucher(string voucherId, int? billId)
		{
            if(billId == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(voucherId))
            {
                var voucher = await _context.Vouchers.GetByID(voucherId);
                if (voucher == null)
                {
                    TempData["Error"] = "Voucher not found";
                    return RedirectToPage("../Index");
                }
                if (voucher.Status == false)
                {
                    TempData["Error"] = "Voucher is not valid!";
                    return RedirectToPage("../Index");
                }
                if (voucher.UsageCount.HasValue && voucher.UsageCount.Value <= 0)
                {
                    TempData["Error"] = "Voucher is not usable!";
                    return RedirectToPage("../Index");
                }
                if (voucher.ExpirationDate.HasValue && voucher.ExpirationDate.Value.Date < DateTime.Now.Date)
                {
                    TempData["Error"] = "Voucher is expired!";
                    return RedirectToPage("../Index");
                }
                Bill bill = await _context.Bills.GetByID(billId.Value);
                bill.VoucherId = voucherId;
                await _context.Bills.Update(bill);
                voucher.UsageCount -= 1;
                await _context.Vouchers.Update(voucher);
            }
            else
            {
                TempData["Error"] = "Please enter voucher";
            }
            
            return RedirectToPage("../Index");
        }

        public async Task<IActionResult> OnGetConfirm(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }
            Bill = await _context.Bills.GetByID(id, false);
            if(Bill == null)
            {
                return NotFound();
            }    
            var billDetails = await _context.BillDetails.GetAll(b => b.BillId == id);

            if (billDetails == null || billDetails.Count() <= 0)
            {
                TempData["Error"] = "Please insert at least 1 product";
                return RedirectToPage("../Index");
            }

            Bill.Status = true;
            await _context.Bills.Update(Bill);
            return RedirectToPage("../Index");
        }

        public async Task<IActionResult> OnGetCancel(int? id)
        {
            var details = await _context.BillDetails.GetAll(d => d.BillId == id, true);
            
            //restore product stock
            //then delete bill detail
            foreach(var detail in details)
            {
                var product = detail.Product;
                Console.WriteLine(product.Id);
                product.Stock += detail.Quantity;
                await _context.Products.Update(detail.Product);
                await _context.BillDetails.Delete((detail.BillId, detail.ProductId));
            }
            await _context.Bills.Delete(id);
            return RedirectToPage("../Index");
        }
    }
}
