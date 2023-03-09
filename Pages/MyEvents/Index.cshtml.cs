using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models;
using EventPlannerApp.Models.Services;
using System.Net;

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
        public MyEventData MyEventD { get; set; }
        public int MyEventID { get; set; }
        public int MenuID { get; set; }

        public async Task OnGetAsync(int? id, int? myeventID)
        {
            MyEventD = new MyEventData();

            MyEventD.MyEvents = await _context.MyEvent
            .Include(b => b.EventType)
            .Include(b=>b.Location)
            .Include(b=>b.Music)
            .Include(b=>b.Photograph)
            .Include(b => b.MyEventMenues)
            .ThenInclude(b => b.Menu)
            .AsNoTracking()
            .ToListAsync();

            if (id != null)
            {
                MyEventID = id.Value;
                MyEvent myevent = MyEventD.MyEvents
                .Where(i => i.ID == id.Value).Single();
                MyEventD.Menues = myevent.MyEventMenues.Select(s => s.Menu);
            }

            //if (_context.MyEvent != null)
            //{
            //    MyEvent = await _context.MyEvent
            //        .Include(b=>b.EventType)
            //        .Include(b=>b.Location)
            //        .Include(b=>b.Music)
            //        .Include(b=>b.Photograph)
            //        .ToListAsync();
            //}
        }
    }
}
