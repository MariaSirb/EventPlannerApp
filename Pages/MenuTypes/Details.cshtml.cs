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
    public class DetailsModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public DetailsModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

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
    }
}
