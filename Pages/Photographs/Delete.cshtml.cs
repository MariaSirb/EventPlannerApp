using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.Photographs
{
    public class DeleteModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DeleteModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Photograph Photograph { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Photograph == null)
            {
                return NotFound();
            }

            var photograph = await _context.Photograph.FirstOrDefaultAsync(m => m.ID == id);

            if (photograph == null)
            {
                return NotFound();
            }
            else 
            {
                Photograph = photograph;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Photograph == null)
            {
                return NotFound();
            }
            var photograph = await _context.Photograph.FindAsync(id);

            if (photograph != null)
            {
                Photograph = photograph;
                _context.Photograph.Remove(Photograph);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
