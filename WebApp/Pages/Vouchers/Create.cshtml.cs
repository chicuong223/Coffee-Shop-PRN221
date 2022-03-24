using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Vouchers
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
            return Page();
        }
        public string Message { get; set; }
        [BindProperty]
        public Voucher Voucher { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if(Voucher.ExpirationDate <= DateTime.Now)
                {
                    TempData["Error"] = "Expiration date must be after today";
                    return Page();
                }
                await _context.Vouchers.Create(Voucher);
            }catch(Exception e)
            {
                Message = "Make sure all inputs are correct";
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
