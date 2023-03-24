using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class Location
    {
        public int ID { get; set; }
        [Display(Name = "Photo")]
        public string LocationImage { get; set; }

        [NotMapped]
        public IFormFile LocationImageFile { get; set; }
        [Display(Name = "Location name")]
        public string LocationName { get; set; }
        [Display(Name = "Location Adress")]
        public string Adress { get; set; }
        [Display(Name = "Maximum Capacitz")]
        public int MaximumCapacity { get; set; }
        [Display(Name = "Location Price")]
        public decimal LocationPrice { get; set; }

        public ICollection<MyEvent>? MyEvents { get; set; }

    }
}
