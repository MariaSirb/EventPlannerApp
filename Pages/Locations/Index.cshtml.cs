using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Data;
using EventPlannerApp.Models.Services;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using EventPlannerApp.Models.Favourite;
using EventPlannerApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace EventPlannerApp.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly EventPlannerApp.Data.EventPlannerAppContext _context;
        private int LogedinClientId;

        public IndexModel(EventPlannerApp.Data.EventPlannerAppContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get; set; } = default!;
   
        public async Task OnGetAsync(bool? showFavourite)
        {
            //string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = _connectionString; 
            ////conn.Open();

            var userEmail = User.Identity.Name;
            var logedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            if (_context.Location != null)
            {
                var locations = _context.Location
                .AsNoTracking();

                if (showFavourite != null && showFavourite == true)
                {
                    locations = locations.Join(
                        _context.FavouriteClientLocation.Where(x=>x.ClientId==LogedinClientId),
                        e => e.ID,
                       f => f.LocationId, (firstentity, secondentity) => new
                {
                    Location = firstentity,
                    FavouriteClientLocation = secondentity
                }).Select(entity => entity.Location);
                   
                }


                Location = await locations.ToListAsync();
            }

            //Verifcam in db Care din event sunt salvate in tabela de fav ( le aduce pe toate in fav event)
            var favLocations = _context.FavouriteClientLocation.Where(x => x.ClientId == logedinClientId).ToList();

            for (int i = 0; i < Location.Count(); i++)
            {
                var currentLocation = Location.ElementAt(i);

                //Aici verifica...Pt event urile din Db se seteaza valoarea pt Addedtofav ca sa seteze Add/Remove to fav
                currentLocation.AddedToFav = favLocations.Where(x => x.ClientId == logedinClientId &&
                  x.LocationId == currentLocation.ID
                ).FirstOrDefault() != null;
            }

        }

        //Aici e metoda pentru butonul add to favourite
        public IActionResult OnPost()
        {
            var userEmail = User.Identity.Name;
            var LogedinClientId = _context.Client.Where(c => c.Email == userEmail).Select(c => c.ID).FirstOrDefault();

            var LocationID = Request.Form["LocationID"];
            var IsAddedtoFav = Request.Form["IsAddedtoFav"];
            var FavLocation = new FavouriteClientLocation();

            FavLocation.LocationId = Int32.Parse(LocationID);
            FavLocation.ClientId = LogedinClientId;

            if (!bool.Parse(IsAddedtoFav)) 
                {
                    _context.FavouriteClientLocation.Add(FavLocation);
                }
            else
                {
                    _context.FavouriteClientLocation.Remove(FavLocation);
                   
            }

            _context.SaveChanges();


            return RedirectToPage("./Index");




            //if (!_context.FavouriteClientLocation.Contains(FavLocation))
            //{
            //    _context.FavouriteClientLocation.Add(FavLocation);
            //    _context.SaveChanges();
            //}

            //return RedirectToPage("./Index");

        }
    }
}
