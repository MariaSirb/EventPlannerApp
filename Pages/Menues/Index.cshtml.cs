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
        private int LogedinClientId;
        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; } = default!;

        public async Task OnGetAsync(bool? showFavourite)
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Menu != null)
            {
                var menues = _context.Menu
                    .Include(b=>b.MenuType)
                    .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    menues = menues.Join(
                        _context.FavouriteClientMenu.Where(x => x.ClientId == LogedinClientId),
                        e => e.ID,
                       f => f.MenuId, (firstentity, secondentity) => new
                       {
                           Menu = firstentity,
                           FavouriteClientMenu = secondentity
                       }).Select(entity => entity.Menu);

                }


                Menu = await menues.ToListAsync();            
            }
        }

        //Aici e metoda pentru butonul add to favourite
        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();
            var MenuID = Request.Form["MenuID"];
            //var ClientID = Request.Form["ClientID"];

            var FavMenu = new FavouriteClientMenu();
            FavMenu.MenuId = Int32.Parse(MenuID);
            FavMenu.ClientId = LogedinClientId;

            if (!_context.FavouriteClientMenu.Contains(FavMenu))
            {
                _context.FavouriteClientMenu.Add(FavMenu);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");

        }
    }
}
