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

        public async Task OnGetAsync(bool? showFavourite)
        {
            var userEmail = User.Identity.Name;
            var logedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Menu != null)
            {
                var menues = _context.Menu
                    .Include(b=>b.MenuType)
                    .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    menues = menues.Join(
                        _context.FavouriteClientMenu.Where(x => x.ClientId == logedinClientId),
                        e => e.ID,
                       f => f.MenuId, (firstentity, secondentity) => new
                       {
                           Menu = firstentity,
                           FavouriteClientMenu = secondentity
                       }).Select(entity => entity.Menu);

                }

                Menu = await menues.ToListAsync();
                //Verifcam in db Care din event sunt salvate in tabela de fav ( le aduce pe toate in fav event)
                var favMenues = _context.FavouriteClientMenu.Where(x => x.ClientId == logedinClientId).ToList();

                for (int i = 0; i < Menu.Count(); i++)
                {
                    var currentMenu = Menu.ElementAt(i);

                    //Aici verifica...Pt event urile din Db se seteaza valoarea pt Addedtofav ca sa seteze Add/Remove to fav
                    currentMenu.AddedToFav = favMenues.Where(x => x.ClientId == logedinClientId &&
                      x.MenuId == currentMenu.ID
                    ).FirstOrDefault() != null;
                }
            }
        }

        //Aici e metoda pentru butonul add to favourite
        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            var LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            var MenuID = Request.Form["MenuID"];
            var IsAddedtoFav = Request.Form["IsAddedtoFav"];
            var FavMenu = new FavouriteClientMenu();

            FavMenu.MenuId = Int32.Parse(MenuID);
            FavMenu.ClientId = LogedinClientId;

            if (!bool.Parse(IsAddedtoFav))
            {
                _context.FavouriteClientMenu.Add(FavMenu);
            }
            else
            {
                _context.FavouriteClientMenu.Remove(FavMenu);

            }

            _context.SaveChanges();


            return RedirectToPage("./Index");

        }
    }
}
