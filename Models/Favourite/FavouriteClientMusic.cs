using EventPlannerApp.Models.Services;

namespace EventPlannerApp.Models.Favourite
{
    public class FavouriteClientMusic
    {
        public int ClientId { get; set; }
        public int MusicId { get; set; }
        public Client Client { get; set; }
        public Music Music { get; set; }
    }
}
