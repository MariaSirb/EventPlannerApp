using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Models.Services;
using EventPlannerApp.Models;
using EventPlannerApp.Models.Favourite;

namespace EventPlannerApp.Data
{
    public class EventPlannerAppContext : DbContext
    {
        public EventPlannerAppContext (DbContextOptions<EventPlannerAppContext> options)
            : base(options)
        {
        }

        public DbSet<EventPlannerApp.Models.Services.EventType> EventType { get; set; } = default!;

        public DbSet<EventPlannerApp.Models.Services.Location> Location { get; set; }

        public DbSet<EventPlannerApp.Models.Services.Music> Music { get; set; }

        public DbSet<EventPlannerApp.Models.Services.Photograph> Photograph { get; set; }

        public DbSet<EventPlannerApp.Models.Services.Menu> Menu { get; set; }

        public DbSet<EventPlannerApp.Models.Services.MenuType> MenuType { get; set; }

        public DbSet<EventPlannerApp.Models.MyEvent> MyEvent { get; set; }

        public DbSet<EventPlannerApp.Models.Client> Client { get; set; }

        public DbSet<EventPlannerApp.Models.Favourite.FavouriteClientEvent> FavouriteClientEvent{ get; set; }
        public DbSet<EventPlannerApp.Models.Favourite.FavouriteClientLocation> FavouriteClientLocation { get; set; }
        public DbSet<EventPlannerApp.Models.Favourite.FavouriteClientMusic> FavouriteClientMusic { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavouriteClientEvent>()
                .HasKey(c => new { c.ClientId, c.MyEventId });

            modelBuilder.Entity<FavouriteClientLocation>()
                .HasKey(c => new { c.ClientId, c.LocationId });

            modelBuilder.Entity<FavouriteClientMusic>()
               .HasKey(c => new { c.ClientId, c.MusicId });
        }
       
    }

}
