using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EventPlannerApp.Pages.Menues
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public CreateModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            ViewData["MenuTypeID"] = new SelectList(_context.Set<MenuType>(), "ID", "TypeName");
            return Page();
        }

        [BindProperty]
        public Menu Menu { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            byte[] bytes = null;
            if (Menu.ItemImageFile != null)
            {
                using (Stream fs = Menu.ItemImageFile.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }

                }
                Menu.ItemImage = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            _context.Menu.Add(Menu);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            //if (!ModelState.IsValid)
            //  {
            //      return Page();
            //  }

            //  _context.Menu.Add(Menu);
            //  await _context.SaveChangesAsync();

            //  return RedirectToPage("./Index");
        }
    }
}
