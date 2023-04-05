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
        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Photograph> Photograph { get;set; } = default!;

        public async Task OnGetAsync(bool? showFavourite)
        {
            var userEmail = User.Identity.Name;
            var logedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Photograph != null)
            {
                var photographs = _context.Photograph
                .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    photographs = photographs.Join(
                        _context.FavouriteClientPhotograph.Where(x => x.ClientId == logedinClientId),
                        e => e.ID,
                       f => f.PhotographId, (firstentity, secondentity) => new
                       {
                           Photograph = firstentity,
                           FavouriteClientPhotograph = secondentity
                       }).Select(entity => entity.Photograph);

                }

                Photograph = await photographs.ToListAsync();

                //Verifcam in db Care din event sunt salvate in tabela de fav ( le aduce pe toate in fav music)
                var favPhotographs = _context.FavouriteClientPhotograph.Where(x => x.ClientId == logedinClientId).ToList();

                for (int i = 0; i < Photograph.Count(); i++)
                {
                    var currentPhotograph = Photograph.ElementAt(i);

                    //Aici verifica...Pt event urile din Db se seteaza valoarea pt Addedtofav ca sa seteze Add/Remove to fav
                    currentPhotograph.AddedToFav = favPhotographs.Where(x => x.ClientId == logedinClientId &&
                      x.PhotographId == currentPhotograph.ID
                    ).FirstOrDefault() != null;
                }
            }
        }
        //Aici e metoda pentru butonul add to favourite
        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            var LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            var PhotographID = Request.Form["PhotographID"];
            var IsAddedtoFav = Request.Form["IsAddedtoFav"];
            var FavPhotograph = new FavouriteClientPhotograph();

            FavPhotograph.PhotographId = Int32.Parse(PhotographID);
            FavPhotograph.ClientId = LogedinClientId;

            if (!bool.Parse(IsAddedtoFav))
            {
                _context.FavouriteClientPhotograph.Add(FavPhotograph);
            }
            else
            {
                _context.FavouriteClientPhotograph.Remove(FavPhotograph);

            }

            _context.SaveChanges();


            return RedirectToPage("./Index");

        }

    }
}
