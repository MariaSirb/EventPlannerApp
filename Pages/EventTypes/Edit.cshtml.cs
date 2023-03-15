using System;
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

namespace EventPlannerApp.Pages.EventTypes
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
        public EventType EventType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EventType == null)
            {
                return NotFound();
            }

            var eventtype =  await _context.EventType.FirstOrDefaultAsync(m => m.ID == id);
            if (eventtype == null)
            {
                return NotFound();
            }
            EventType = eventtype;
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

            _context.Attach(EventType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTypeExists(EventType.ID))
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

        private bool EventTypeExists(int id)
        {
          return _context.EventType.Any(e => e.ID == id);
        }
    }
}
