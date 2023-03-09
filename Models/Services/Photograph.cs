using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlannerApp.Models.Services
{
    public class Photograph
    {
        public int ID { get; set; }
        public string PhotographName { get; set; }

        public decimal PhotographPrice { get; set; }
        public string PhotographImage { get; set; }
        [NotMapped]
        public IFormFile PhotographImageFile { get; set; }

        public ICollection<MyEvent>? MyEvents { get; set; }

    }
}
