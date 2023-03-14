using System;
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
    public class DeleteModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DeleteModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public MyEvent MyEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MyEvent == null)
            {
                return NotFound();
            }

            var myevent = await _context.MyEvent
                .Include(b=>b.EventType)
                .Include(b=>b.Location)
                .Include(b=>b.Music)
                .Include(b=>b.Photograph)
                .Include(b=>b.MyEventMenues)
                .ThenInclude(b=>b.Menu)
                .FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MyEvent == null)
            {
                return NotFound();
            }
            var myevent = await _context.MyEvent.FindAsync(id);

            if (myevent != null)
            {
                MyEvent = myevent;
                _context.MyEvent.Remove(MyEvent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
