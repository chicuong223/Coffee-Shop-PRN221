using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.RepositoryInterface;

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
        //ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
            ViewData["VoucherId"] = new SelectList(await _context.Vouchers.GetAll(null), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bill Bill { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.Bills.Create(Bill);

            return RedirectToPage("./Index");
        }
    }
}
