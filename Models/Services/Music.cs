using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlannerApp.Models.Services
{
    public class Music
    {
        public int ID { get; set; }
        public string DjName { get; set; }

        public decimal DjPrice { get; set; }

        public string DjImage { get; set; }

        [NotMapped]
        public IFormFile DjImageFile { get; set; }

        public ICollection<MyEvent>? MyEvents { get; set; }
    }
}
