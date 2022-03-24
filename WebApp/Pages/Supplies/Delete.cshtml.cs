﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using System;

namespace WebApp.Pages.Supplies
{
    public class DeleteModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DeleteModel(IRepoWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public Supply Supply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productid, int? supplierid, DateTime? supplydate)
        {
            if (productid == null || supplierid == null || !supplydate.HasValue)
            {
                return NotFound();
            }

            Supply = await _context.Supplies.GetByID((supplierid, productid, supplydate));

            if (Supply == null)
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

            Supply = await _context.Supplies.GetByID(id, false);

            if (Supply != null)
            {
                await _context.Supplies.Delete(Supply);
            }

            return RedirectToPage("./Index");
        }
    }
}
