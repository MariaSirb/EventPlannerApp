using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Pages.MenuTypes
{
    public class EditModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public EditModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MenuType MenuType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MenuType == null)
            {
                return NotFound();
            }

            var menutype =  await _context.MenuType.FirstOrDefaultAsync(m => m.ID == id);
            if (menutype == null)
            {
                return NotFound();
            }
            MenuType = menutype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MenuType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuTypeExists(MenuType.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MenuTypeExists(int id)
        {
          return _context.MenuType.Any(e => e.ID == id);
        }
    }
}
