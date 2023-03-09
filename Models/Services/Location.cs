using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlannerApp.Models.Services
{
    public class Location
    {
        public int ID { get; set; }

        public string LocationImage { get; set; }

        [NotMapped]
        public IFormFile LocationImageFile { get; set; }

        public string LocationName { get; set; }
        public string Adress { get; set; }
        public int MaximumCapacity { get; set; }
        public decimal LocationPrice { get; set; }

        

    }
}
