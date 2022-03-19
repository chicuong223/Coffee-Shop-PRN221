using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using WebApp.Utilities;

namespace WebApp.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IRepoWrapper _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IRepoWrapper context
            , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.GetByID(id, true);
            //Product = await _context.Products
            //    .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _context.Categories.GetAll(ca => ca.Status == true), "Id", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            ISession session = HttpContext.Session;
            var currentUsername = session.GetString("Username");
            var role = session.GetString("Role");
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(role))
            {
                return RedirectToPage("../Authenticate/Login");
            }
            if (!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }
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
                await _context.Products.Update(Product);
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
            return _context.Products.GetByID(id, false) != null;
        }
    }
}
