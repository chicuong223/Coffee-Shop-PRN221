﻿using System;
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

        public async Task<IActionResult> OnGet([FromQuery(Name = "billid")] int? billId, [FromQuery(Name = "productid")] int? productId)
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
            if (billId == null)
            {
                return RedirectToPage("../Unauthorized");
            }
            var billExists = await _context.Bills.GetByID(billId.Value) != null;
            if (!billExists)
            {
                return RedirectToPage("../Bills/Index");
            }
            var product = await _context.Products.GetByID(productId.Value);
            if(product.Stock <= 0)
            {
                TempData["Error"] = "This product is out of stock";
                return RedirectToPage("../Index");
            }

            var details = await _context.BillDetails.GetByID((billId.Value, productId.Value));
            if(details == null)
            {
                BillDetail billDetail = new BillDetail();
                billDetail.BillId = billId.Value;
                billDetail.ProductId = productId.Value;
                billDetail.Quantity = 1;
                billDetail.UnitPrice = product.Price;
                billDetail.SubTotal = billDetail.Quantity * billDetail.UnitPrice;
                await _context.BillDetails.Create(billDetail);
                product.Stock -= billDetail.Quantity;
                await _context.Products.Update(product);
                return RedirectToPage("../Index");
            }
            else
            {
                TempData["Error"] = "Duplicated product";
                return RedirectToPage("../Index");
            }
            
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
                TempData["DetailError"] = "Please fill in all fields";
                return RedirectToPage("./Create", new { billId = billId.Value });
            }

            var product = await _context.Products.GetByID(BillDetail.ProductId);
            if (product == null)
            {
                TempData["Error"] = "This product is not available";
                return RedirectToPage("../Index");
            }
            var detail = await _context.BillDetails.GetByID((billId.Value, product.Id), false);
            if (detail != null)
            {
                BillDetail.Quantity += detail.Quantity;

                BillDetail.UnitPrice = product.Price;
                BillDetail.SubTotal = product.Price * BillDetail.Quantity;
                BillDetail.BillId = billId.Value;

                //check quantity vs unit in stock
                var quantity = product.Stock;
                if (quantity == 0 || BillDetail.Quantity > quantity)
                {
                    TempData["DetailError"] = $"This product only has {quantity} units in stock";
                    return RedirectToPage("./Create", new { billid = billId.Value });
                }
                await _context.BillDetails.Update(BillDetail);
            }
            else
            {
                BillDetail.UnitPrice = product.Price;
                BillDetail.SubTotal = product.Price * BillDetail.Quantity;
                BillDetail.BillId = billId.Value;

                //check quantity vs unit in stock
                var quantity = product.Stock;
                if (quantity == 0 || BillDetail.Quantity > quantity)
                {
                    TempData["DetailError"] = $"This product only has {quantity} units in stock";
                    return RedirectToPage("./Create", new { billid = billId.Value });
                }
                await _context.BillDetails.Create(BillDetail);
            }

            //update stock of products
            product.Stock -= BillDetail.Quantity;
            await _context.Products.Update(product);

            return RedirectToPage("../Bills/Edit", new { id = BillDetail.BillId });
        }
    }
}
