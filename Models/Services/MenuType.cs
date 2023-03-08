namespace EventPlannerApp.Models.Services
{
    public class MenuType
    {
        public int ID { get; set; }

        public string TypeName { get; set; }

        public ICollection<Menu>? Menues { get; set; }
    }
}
