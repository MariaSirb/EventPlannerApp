namespace EventPlannerApp.Models.Services
{
    public class MyEventMenu
    {

    public int ID { get; set; }
        public int MyEventID { get; set; }
        public MyEvent MyEvent { get; set; }
        public int MenuID { get; set; }
        public Menu Menu { get; set; }

    }
}
