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
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<MyEvent> MyEvent { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MyEvent != null)
            {
                MyEvent = await _context.MyEvent
                    .Include(b=>b.EventType)
                    .ToListAsync();
            }
        }
    }
}
