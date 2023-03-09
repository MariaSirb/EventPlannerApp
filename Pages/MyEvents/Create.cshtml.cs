using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventPlannerApp.Data;
using EventPlannerApp.Models;
using System.Security.Policy;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.MyEvents
{
    public class CreateModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public CreateModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["EventTypeID"] = new SelectList(_context.Set<EventType>(), "ID", "EventTypeName");
            return Page();
        }

        [BindProperty]
        public MyEvent MyEvent { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MyEvent.Add(MyEvent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
