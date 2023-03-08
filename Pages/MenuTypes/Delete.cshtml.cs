using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.MenuTypes
{
    public class DeleteModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DeleteModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public MenuType MenuType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MenuType == null)
            {
                return NotFound();
            }

            var menutype = await _context.MenuType.FirstOrDefaultAsync(m => m.ID == id);

            if (menutype == null)
            {
                return NotFound();
            }
            else 
            {
                MenuType = menutype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MenuType == null)
            {
                return NotFound();
            }
            var menutype = await _context.MenuType.FindAsync(id);

            if (menutype != null)
            {
                MenuType = menutype;
                _context.MenuType.Remove(MenuType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
