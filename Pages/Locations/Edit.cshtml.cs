﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EventPlannerApp.Pages.Locations
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public EditModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Location Location { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location =  await _context.Location.FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }
            Location = location;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(Location.ID))
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

        private bool LocationExists(int id)
        {
          return _context.Location.Any(e => e.ID == id);
        }
    }
}
