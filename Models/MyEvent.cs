//using EventPlannerApp.Migrations;
using EventPlannerApp.Models.Services;
using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //Relatia cu Tipul de eveniment
        public int? EventTypeID { get; set; }
        public EventType? EventType { get; set; }

        //Relatia cu Locatiile
        public int? LocationID { get; set; }
        public Location? Location { get; set; }

        //Relatie cu Muzica

        public int? MusicID { get; set; }
        public Music? Music { get; set; }

        //Relatie cu Photograph

        public int? PhotographID { get; set; }
        public Photograph? Photograph { get; set; }

        //Relatie cu Menu

        public ICollection<MyEventMenu>? MyEventMenues { get; set; }

        //Relatia cu clientii

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        [NotMapped]
        public bool AddedToFav { get; set; }


    }
}
