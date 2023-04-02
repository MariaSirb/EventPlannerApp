using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Models.Favourite
{
    public class FavouriteClientMenu
    {
        public int ClientId { get; set; }
        public int MenuId { get; set; }
        public Client Client { get; set; }
        public Menu Menu { get; set; }
    }
}
