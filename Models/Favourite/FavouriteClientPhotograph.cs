using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Models.Favourite
{
    public class FavouriteClientPhotograph
    {
        public int ClientId { get; set; }
        public int PhotographId { get; set; }
        public Client Client { get; set; }
        public Photograph Photograph { get; set; }
    }
}
