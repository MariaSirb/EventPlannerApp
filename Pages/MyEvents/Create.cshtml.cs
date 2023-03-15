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

namespace EventPlannerApp.Pages.MyEvents
{
    public class CreateModel : MyEventMenuesPageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public CreateModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["EventTypeID"] = new SelectList(_context.Set<EventType>(), "ID", "EventTypeName");
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "LocationName");
            ViewData["MusicID"] = new SelectList(_context.Set<Music>(), "ID", "DjName");
            ViewData["PhotographID"] = new SelectList(_context.Set<Photograph>(), "ID", "PhotographName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
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
            //if (newMyEvent.EndDate < newMyEvent.StartDate)
            //{
            //    return RedirectToPage("./Index");
            //}
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
