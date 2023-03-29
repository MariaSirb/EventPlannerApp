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
        private int LogedinClientId;
        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Music> Music { get;set; } = default!;

        public async Task OnGetAsync(bool? showFavourite)
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Music != null)
            {
                var musics = _context.Music
                .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    musics = musics.Join(
                        _context.FavouriteClientMusic.Where(x => x.ClientId == LogedinClientId),
                        e => e.ID,
                       f => f.MusicId, (firstentity, secondentity) => new
                       {
                           Music = firstentity,
                           FavouriteClientMusic = secondentity
                       }).Select(entity => entity.Music);

                }


                Music = await musics.ToListAsync();
            }
            //if (_context.Music != null)
            //{
            //    Music = await _context.Music.ToListAsync();
            //}
        }

        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();
            var MusicID = Request.Form["MusicID"];
            //var ClientID = Request.Form["ClientID"];

            var FavMusic = new FavouriteClientMusic();
            FavMusic.MusicId = Int32.Parse(MusicID);
            FavMusic.ClientId = LogedinClientId;

            if (!_context.FavouriteClientMusic.Contains(FavMusic))
            {
                _context.FavouriteClientMusic.Add(FavMusic);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");

        }
    }
}
