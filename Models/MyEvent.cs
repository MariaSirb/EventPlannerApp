//using EventPlannerApp.Migrations;
using EventPlannerApp.Models.Services;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerApp.Models
{
    public class MyEvent
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Alte Mentiuni ( ex. flori, culori, tematica etc)")]
        public string Mention { get; set; }

        public int? EventTypeID { get; set; }
        public EventType? EventType { get; set; }
    }
}
