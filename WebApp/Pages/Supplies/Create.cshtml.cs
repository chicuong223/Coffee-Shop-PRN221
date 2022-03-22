using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Supplies
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public CreateModel(IRepoWrapper context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            Product = await _context.Products.GetByID(ProductId);
            if (Product == null)
            {
                return NotFound();
            }
        //ViewData["ProductId"] = new SelectList(_context.Products.GetAll(null).Result.ToList(), "Id", "ProductName");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.GetAll(null).Result.ToList(), "Id", "Name");
            Now = DateTime.Now;
            return Page();
        }
        [BindProperty(SupportsGet =true)]
        public string Message { get; set; }

        [BindProperty]
        public DateTime Now { get; set; }

        public Product Product { get; set; }
        [BindProperty]
        public Supply Supply { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", new { ProductId = Product.Id });
            }
            //Supply.SupplyDate = DateTime.Now;
            Product = await _context.Products.GetByID(Supply.ProductId);
            try
            {
                await _context.Supplies.Create(Supply);
            }catch(Exception e)
            {
                return RedirectToPage("./Create", new { ProductId = Product.Id , Message=$"Just added more {Product.ProductName}, please wait a minute!"});
            }

            
            Product.Stock += Supply.Quantity;
            await _context.Products.Update(Product);

            return RedirectToPage("../Products/Index");
        }
    }
}
