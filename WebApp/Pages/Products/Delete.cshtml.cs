﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Product = await _context.Products
            //    .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            Product = await _context.Products.GetByID(id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Product = await _context.Products.FindAsync(id);

            Product = await _context.Products.GetByID(id.Value);

            if (Product != null)
            {
                if(!Product.Status.Value)
                {
                    TempData["Error"] = "This product is already inactive!";
                    return await OnGetAsync(Product.Id);
                }
                //_context.Products.Remove(Product);
                //await _context.SaveChangesAsync();
                await _context.Products.Delete(Product.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
