using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;
using Microsoft.CodeAnalysis;

namespace EventPlannerApp.Pages.Locations
{
    public class CreateModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;

        public CreateModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Services.Location Location { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            byte[] bytes = null;
            if (Location.LocationImageFile != null)
            {
                using (Stream fs = Location.LocationImageFile.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }

                }
                Location.LocationImage = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            _context.Location.Add(Location);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");


            //if (!ModelState.IsValid)
            //  {
            //      return Page();
            //  }

            //  _context.Location.Add(Location);
            //  await _context.SaveChangesAsync();

            //  return RedirectToPage("./Index");
        }
    }
}
