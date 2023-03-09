namespace EventPlannerApp.Models.Services
{
    public class MyEventData
    {
        public IEnumerable<MyEvent> MyEvents { get; set; }
        public IEnumerable<Menu> Menues { get; set; }
        public IEnumerable<MyEventMenu> MyEventMenues { get; set; }
    }
}
