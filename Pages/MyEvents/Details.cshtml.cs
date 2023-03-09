﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models;

namespace EventPlannerApp.Pages.MyEvents
{
    public class DetailsModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DetailsModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

      public MyEvent MyEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MyEvent == null)
            {
                return NotFound();
            }

            var myevent = await _context.MyEvent.FirstOrDefaultAsync(m => m.ID == id);
            if (myevent == null)
            {
                return NotFound();
            }
            else 
            {
                MyEvent = myevent;
            }
            return Page();
        }
    }
}
