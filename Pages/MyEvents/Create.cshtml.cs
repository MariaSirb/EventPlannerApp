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
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerApp.Pages.MyEvents
{
    public class CreateModel : MyEventMenuesPageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(EventPlannerApp.Data.EventPlannerAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {   // urm 2 linii pentru client - aia sa apara evenimentele fiecarui client in parte si adminul sa le vada pe toate
            var userEmail = User.Identity.Name; //email of the connected user
            int currentClientID = _context.Client.First(client => client.Email == userEmail).ID;

            var userName = _userManager.GetUserName(User);

            var detaliiClient = _context.Client
                .Where(c => c.Email == userName)
                .Select(x => new
                {
                    x.ID,
                    DetaliiClient = x.FirstName + " " + x.LastName
                });



            ViewData["EventTypeID"] = new SelectList(_context.Set<EventType>(), "ID", "EventTypeName");
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "LocationName");
            ViewData["MusicID"] = new SelectList(_context.Set<Music>(), "ID", "DjName");
            ViewData["PhotographID"] = new SelectList(_context.Set<Photograph>(), "ID", "PhotographName");
            ViewData["ClientID"] = new SelectList(detaliiClient, "ID", "DetaliiClient", currentClientID);

            var myevent = new MyEvent();
            
            myevent.MyEventMenues = new List<MyEventMenu>();
            PopulateAssignedMenuData(_context, myevent);

            return Page();
        }

        [BindProperty]
        public MyEvent MyEvent { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedMenues)
        {
            var newMyEvent = new MyEvent();
            if (selectedMenues != null)
            {
                newMyEvent.MyEventMenues = new List<MyEventMenu>();
                foreach (var cat in selectedMenues)
                {
                    var catToAdd = new MyEventMenu
                    {
                        MenuID = int.Parse(cat)
                    };
                    newMyEvent.MyEventMenues.Add(catToAdd);
                }
            }
            
             //Restrictie 1 la Data
            if (MyEvent.EndDate < MyEvent.StartDate)
            {
              return RedirectToPage("./Create");
            }

            MyEvent.MyEventMenues = newMyEvent.MyEventMenues;
            _context.MyEvent.Add(MyEvent);
            await _context.SaveChangesAsync();
              return RedirectToPage("./Index");
        
         PopulateAssignedMenuData(_context, newMyEvent);
            return Page();

        //if (!ModelState.IsValid)
        //  {
        //      return Page();
        //  }

        //  _context.MyEvent.Add(MyEvent);
        //  await _context.SaveChangesAsync();

        //  return RedirectToPage("./Index");
    }
    }
}
