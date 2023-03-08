using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.Menues
{
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                Menu = await _context.Menu
                    .Include(b=>b.MenuType)
                    .ToListAsync();
            }
        }
    }
}
