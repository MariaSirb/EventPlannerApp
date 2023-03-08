using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.Musics
{
    public class DetailsModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DetailsModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

      public Music Music { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FirstOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }
            else 
            {
                Music = music;
            }
            return Page();
        }
    }
}
