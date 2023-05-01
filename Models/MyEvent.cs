//using EventPlannerApp.Migrations;
using EventPlannerApp.Models.Services;
using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlannerApp.Models
{
    public class MyEvent/*: IValidatableObject*/
    {
        public int ID { get; set; }
        [Display(Name = "Start Date: ")]
        [Required(ErrorMessage = "Please add StartDate to the request.")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date: ")]
        [Required(ErrorMessage = "Please add EndDate to the request.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }   

        [Display(Name = "Mention ( ex. flowers, colors, topics etc)")]
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


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (EndDate <= StartDate)
        //    {
        //        yield return new ValidationResult(
        //            $"End date must be greater than the start date.",
        //            new[] { nameof(EndDate) });
        //    }
        //}

    }
}
