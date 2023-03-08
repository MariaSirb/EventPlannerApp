using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventPlannerApp.Models.Services;

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
    }
}
