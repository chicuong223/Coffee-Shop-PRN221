using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using WebApp.Utilities;

namespace WebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IRepoWrapper _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(IRepoWrapper context
            , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
            if (!role.Equals("Admin"))
            {
                return RedirectToPage("../Error");
            }
            var categories = await _context.Categories.GetAll(ca => ca.Status == true);
            foreach(var category in categories)
            {
                Console.WriteLine(category.Id);
            }
            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            try
            {
                if (image != null)
                {
                    await FileUtility.UploadFile(image, _environment);
                    Product.ImageURL = image.FileName;
                }

                Product.Status = true;

                await _context.Products.Create(Product);
            }
            catch(Exception ex)
            {
                ViewData["Error"] = ex.Message; 
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
