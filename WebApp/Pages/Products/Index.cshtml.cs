﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;
using X.PagedList;

namespace WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IBaseRepository<Product> _context;

        public IndexModel(IBaseRepository<Product> context)
        {
            _context = context;
        }

        public IPagedList<Product> Products { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Products = await _context.GetList(null, true, pageIndex);
        }
    }
}
