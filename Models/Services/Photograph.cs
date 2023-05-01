using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class Photograph
    {
        public int ID { get; set; }
        [Display(Name = "Photograph name: ")]
        public string PhotographName { get; set; }
        [Display(Name = "Photograph price/evet (euro)")]
        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]

        public decimal PhotographPrice { get; set; }
        [Display(Name = "Photo")]
        public string PhotographImage { get; set; }
        [NotMapped]
        public IFormFile PhotographImageFile { get; set; }

        public ICollection<MyEvent>? MyEvents { get; set; }

        [NotMapped]
        public bool AddedToFav { get; set; }

    }
}
