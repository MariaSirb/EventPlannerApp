using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EventPlannerApp.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Your impression of the event")]
        public string Description { get; set; }

        //public string eventType

        [Display(Name = "Quality of our services: Bad, Good, Great, Excelent")]
        public string ServiceQuality { get; set; }

        [Display(Name = "Select the date when you write this review")]

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

    }
}
