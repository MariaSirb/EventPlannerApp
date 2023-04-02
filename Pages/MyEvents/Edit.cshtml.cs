using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.MyEvents
{
    public class EditModel : MyEventMenuesPageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public EditModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyEvent MyEvent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MyEvent == null)
            {
                return NotFound();
            }

            var myevent =  await _context.MyEvent
                .Include(b=>b.EventType)
                .Include(b=>b.Location)
                .Include(b=>b.Music)
                .Include(b=>b.Photograph)
                .Include(b=>b.MyEventMenues).ThenInclude(b=>b.Menu)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (myevent == null)
            {
                return NotFound();
            }
            PopulateAssignedMenuData(_context, MyEvent);
            MyEvent = myevent;

            ViewData["EventTypeID"] = new SelectList(_context.Set<EventType>(), "ID", "EventTypeName");
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "LocationName");
            ViewData["MusicID"] = new SelectList(_context.Set<Music>(), "ID", "DjName");
            ViewData["PhotographID"] = new SelectList(_context.Set<Photograph>(), "ID", "PhotographName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedMenues)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var myeventToUpdate = await _context.MyEvent
            .Include(i => i.EventType)
            .Include(b => b.Location)
            .Include(b => b.Music)
            .Include(b => b.Photograph)
            .Include(b => b.MyEventMenues).ThenInclude(b => b.Menu)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (myeventToUpdate == null)
            {
                return NotFound();
            }
            

            if (await TryUpdateModelAsync<MyEvent>(
            myeventToUpdate,
            "MyEvent",
            i => i.StartDate, i => i.EndDate,
            i => i.Mention, i => i.EventTypeID, i => i.LocationID,
            i => i.MusicID, i => i.PhotographID))
            {
                UpdateMyEventMenues(_context, selectedMenues, myeventToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            //if (MyEvent.EndDate < MyEvent.StartDate)
            //{
            //    return RedirectToPage("./Edit");
            //}
            //Apelam UpdateMyEventMenues pentru a aplica informatiile din checkboxuri la entitatea MyEvents care
            //este editata
            UpdateMyEventMenues(_context, selectedMenues, myeventToUpdate);
            PopulateAssignedMenuData(_context, myeventToUpdate);
            return Page();
        
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        //_context.Attach(MyEvent).State = EntityState.Modified;

        //try
        //{
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!MyEventExists(MyEvent.ID))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
        //}

        //return RedirectToPage("./Index");
    }

        private bool MyEventExists(int id)
        {
          return _context.MyEvent.Any(e => e.ID == id);
        }
    }
}
