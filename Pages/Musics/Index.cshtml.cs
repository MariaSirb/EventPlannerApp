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

namespace EventPlannerApp.Pages.Musics
{
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;
        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Music> Music { get;set; } = default!;

        public async Task OnGetAsync(bool? showFavourite)
        {

            if (_context.Music != null)
            {
                var userEmail = User.Identity.Name;
                var logedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

                var musics = _context.Music
                .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    musics = musics.Join(
                        _context.FavouriteClientMusic.Where(x => x.ClientId == logedinClientId),
                        e => e.ID,
                       f => f.MusicId, (firstentity, secondentity) => new
                       {
                           Music = firstentity,
                           FavouriteClientMusic = secondentity
                       }).Select(entity => entity.Music);

                }

                Music = await musics.ToListAsync();

                //Verifcam in db Care din event sunt salvate in tabela de fav ( le aduce pe toate in fav music)
                var favMusics = _context.FavouriteClientMusic.Where(x => x.ClientId == logedinClientId).ToList();

                for (int i = 0; i < Music.Count(); i++)
                {
                    var currentMusic = Music.ElementAt(i);

                    //Aici verifica...Pt event urile din Db se seteaza valoarea pt Addedtofav ca sa seteze Add/Remove to fav
                    currentMusic.AddedToFav = favMusics.Where(x => x.ClientId == logedinClientId &&
                      x.MusicId == currentMusic.ID
                    ).FirstOrDefault() != null;
                }
            }
            
        }

        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            var LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            var MusicID = Request.Form["MusicID"];
            var IsAddedtoFav = Request.Form["IsAddedtoFav"];
            var FavMusic = new FavouriteClientMusic();

            FavMusic.MusicId = Int32.Parse(MusicID);
            FavMusic.ClientId = LogedinClientId;

            if (!bool.Parse(IsAddedtoFav))
            {
                _context.FavouriteClientMusic.Add(FavMusic);
            }
            else
            {
                _context.FavouriteClientMusic.Remove(FavMusic);

            }

            _context.SaveChanges();


            return RedirectToPage("./Index");


        }
    }
}
