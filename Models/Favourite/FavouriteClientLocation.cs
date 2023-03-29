using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Models.Favourite
{
    public class FavouriteClientLocation
    {
        public int ClientId { get; set; }
        public int LocationId { get; set; }
        public Client Client { get; set; }
        public Location Location { get; set; }
    }
}
