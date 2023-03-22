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
using Microsoft.AspNetCore.Identity;
using EventPlannerApp.Models.Favourite;

namespace EventPlannerApp.Pages.MyEvents
{
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        // urm linie pentru client - aia sa apara evenimentele fiecarui client in parte si adminul sa le vada pe toate
        private readonly string ADMIN_EMAIL = "admin@gmail.com";

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
            .Include(b=>b.Client)
            .AsNoTracking()
            .ToListAsync();

            //urmatoarele 3 linii pentru client/admin - aia sa apara evenimentele fiecarui client in parte si adminul sa le vada pe toate
            var userEmail = User.Identity.Name;
            if (userEmail != ADMIN_EMAIL) 
                MyEventD.MyEvents = MyEventD.MyEvents.Where(myEvent => myEvent.Client?.Email == userEmail);

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


        public IActionResult OnPost()
        {
            var EventID = Request.Form["EventID"];
            var ClientID = Request.Form["ClientID"];

            var FavEvent = new FavouriteClientEvent();
            FavEvent.MyEventId = Int32.Parse(EventID);
            FavEvent.ClientId = Int32.Parse(ClientID);

            if (!_context.FavouriteClientEvent.Contains(FavEvent))
            {
                _context.FavouriteClientEvent.Add(FavEvent);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");

        }
    }
}
