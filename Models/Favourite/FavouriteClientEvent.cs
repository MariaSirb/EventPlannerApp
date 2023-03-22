namespace EventPlannerApp.Models.Favourite
{
    public class FavouriteClientEvent
    {
        public int ClientId { get; set; }
        public int MyEventId { get; set; }
        public Client Client { get; set; }
        public MyEvent MyEvent { get; set; }

    }
}
