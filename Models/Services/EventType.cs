namespace EventPlannerApp.Models.Services
{
    public class EventType
    {
        public int ID { get; set; }
        public string EventTypeName { get; set; }

       public ICollection<MyEvent>? MyEvents { get; set; }


    }
}
