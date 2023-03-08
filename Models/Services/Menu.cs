using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlannerApp.Models.Services
{
    public class Menu
    {
        public int ID { get; set; }
        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }
        public string ItemImage { get; set; }
        [NotMapped]
        public IFormFile ItemImageFile { get; set; }

        // fac legatura cu Tipurile din meniu (mancare, bautura, prajituri, saratele)

        public int? MenuTypeID { get; set; }
        public MenuType? MenuType { get; set; }

    }
}
