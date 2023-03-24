using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class Music
    {
        public int ID { get; set; }
        [Display(Name = "Dj Name")]
        public string DjName { get; set; }
        [Display(Name = "Dj price/event")]
        public decimal DjPrice { get; set; }
        [Display(Name = "Photo")]
        public string DjImage { get; set; }

        [NotMapped]
        public IFormFile DjImageFile { get; set; }

        public ICollection<MyEvent>? MyEvents { get; set; }
    }
}
