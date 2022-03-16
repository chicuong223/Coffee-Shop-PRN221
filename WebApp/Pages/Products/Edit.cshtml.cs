﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;
using WebApp.Utilities;

namespace WebApp.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IBaseRepository<Product> _context;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IBaseRepository<Product> context
            , IBaseRepository<Category> categoryRepository
            , IWebHostEnvironment environment)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.GetByID(id, true);
            //Product = await _context.Products
            //    .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(ca => ca.Status == true), "Id", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Product).State = EntityState.Modified;

            try
            {
                if(image != null)
                {
                    await FileUtility.UploadFile(image, _environment);
                    Product.ImageURL = image.FileName;
                }
                //await _context.SaveChangesAsync();
                await _context.Update(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.GetByID(id, false) != null;
        }
    }
}
