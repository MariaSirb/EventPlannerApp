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
using System.Globalization;

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
    
        public IList<MyEvent> MyEvent { get; set; } = default!;
        public MyEventData MyEventD { get; set; }
        public int MyEventID { get; set; }
        public int MenuID { get; set; }

        public async Task OnGetAsync(int? id, int? myeventID, bool? showFavourite)
        {
            MyEventD = new MyEventData();

            var events = _context.MyEvent
            .Include(b => b.EventType)
            .Include(b => b.Location)
            .Include(b => b.Music)
            .Include(b => b.Photograph)
            .Include(b => b.MyEventMenues)
            .ThenInclude(b => b.Menu)
            .Include(b => b.Client)
            .AsNoTracking();

            if (showFavourite != null && showFavourite == true)
            {
                events = events.Join(_context.FavouriteClientEvent, e => e.ID,
                f => f.MyEventId, (firstentity, secondentity) => new
                {
                    MyEvent = firstentity,
                    FavouriteClientEvent = secondentity
                }).Select(entity => entity.MyEvent);
            }

            MyEventD.MyEvents = await events.ToListAsync();

            var userEmail = User.Identity.Name;

            var logedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            //Verifcam in db Care din event sunt salvate in tabela de fav ( le aduce pe toate in fav event)
            var favEvents = _context.FavouriteClientEvent.Where(x => x.ClientId == logedinClientId).ToList();

            for (int i = 0; i < MyEventD.MyEvents.Count(); i++)
            {
                var currentEvent = MyEventD.MyEvents.ElementAt(i);

                //Aici verifica...Pt event urile din Db se seteaza valoarea pt Addedtofav ca sa seteze Add/Remove to fav
                currentEvent.AddedToFav = favEvents.Where(x => x.ClientId == logedinClientId &&
                   x.MyEventId == currentEvent.ID
                ).FirstOrDefault() != null;
            }


            //urmatoarele 2 linii pentru client/admin - aia sa apara evenimentele fiecarui client in parte si adminul sa le vada pe toate
            if (userEmail != ADMIN_EMAIL)
                MyEventD.MyEvents = MyEventD.MyEvents.Where(myEvent => myEvent.Client?.Email == userEmail);

            if (id != null)
            {
                MyEventID = id.Value;
                MyEvent myevent = MyEventD.MyEvents
                .Where(i => i.ID == id.Value).Single();
                MyEventD.Menues = myevent.MyEventMenues.Select(s => s.Menu);
            }

        }


        public IActionResult OnPost()
        {
            var EventID = Request.Form["EventID"];
            var ClientID = Request.Form["ClientID"];
            var IsAddedtoFav = Request.Form["IsAddedtoFav"];
            var FavEvent = new FavouriteClientEvent();

            FavEvent.MyEventId = Int32.Parse(EventID);
            FavEvent.ClientId = Int32.Parse(ClientID);

            if (!bool.Parse(IsAddedtoFav))
            {
                _context.FavouriteClientEvent.Add(FavEvent);
            }
            else
            {
                _context.FavouriteClientEvent.Remove(FavEvent);
            }

            _context.SaveChanges();


            return RedirectToPage("./Index");

        }
    }
}
