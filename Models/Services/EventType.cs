using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class EventType
    {
        public int ID { get; set; }
        [Display(Name = "Event Type: ")]
        public string EventTypeName { get; set; }
        [Display(Name = " Short Description: ")]
        public string EventDescription { get; set; }    
       public ICollection<MyEvent>? MyEvents { get; set; }


    }
}
