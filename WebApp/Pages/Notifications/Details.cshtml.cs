﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace WebApp.Pages.Notifications
{
    public class DetailsModel : PageModel
    {
        private readonly IRepoWrapper _context;

        public DetailsModel(IRepoWrapper context)
        {
            _context = context;
        }

        public Notification Notification { get; set; }

        public IList<NotificationDetail> Details { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notifications.GetByID(id);

            if (Notification == null)
            {
                return NotFound();
            }
            Details = (await _context.NotificationDetails.GetAll(d => d.NotificationId == id, true)).ToList();
            return Page();
        }
    }
}
