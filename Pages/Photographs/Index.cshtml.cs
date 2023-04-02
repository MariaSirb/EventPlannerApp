using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;
using EventPlannerApp.Models.Favourite;

namespace EventPlannerApp.Pages.Photographs
{
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;
        private int LogedinClientId;
        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Photograph> Photograph { get;set; } = default!;

        public async Task OnGetAsync(bool? showFavourite)
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Photograph != null)
            {
                var photographs = _context.Photograph
                .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    photographs = photographs.Join(
                        _context.FavouriteClientPhotograph.Where(x => x.ClientId == LogedinClientId),
                        e => e.ID,
                       f => f.PhotographId, (firstentity, secondentity) => new
                       {
                           Photograph = firstentity,
                           FavouriteClientPhotograph = secondentity
                       }).Select(entity => entity.Photograph);

                }

                Photograph = await photographs.ToListAsync();
            }
        }

        //Aici e metoda pentru butonul add to favourite
        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();
            var PhotographID = Request.Form["PhotographID"];
            //var ClientID = Request.Form["ClientID"];

            var FavPhotograph = new FavouriteClientPhotograph();
            FavPhotograph.PhotographId = Int32.Parse(PhotographID);
            FavPhotograph.ClientId = LogedinClientId;

            if (!_context.FavouriteClientPhotograph.Contains(FavPhotograph))
            {
                _context.FavouriteClientPhotograph.Add(FavPhotograph);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");

        }

    }
}
