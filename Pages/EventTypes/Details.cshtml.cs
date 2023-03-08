using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.EventTypes
{
    public class DetailsModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DetailsModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

      public EventType EventType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EventType == null)
            {
                return NotFound();
            }

            var eventtype = await _context.EventType.FirstOrDefaultAsync(m => m.ID == id);
            if (eventtype == null)
            {
                return NotFound();
            }
            else 
            {
                EventType = eventtype;
            }
            return Page();
        }
    }
}
