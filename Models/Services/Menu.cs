using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class Menu
    {
        public int ID { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Photo")]
        public string ItemImage { get; set; }
        [NotMapped]
        public IFormFile ItemImageFile { get; set; }

        // fac legatura cu Tipurile din meniu (mancare, bautura, prajituri, saratele)

        public int? MenuTypeID { get; set; }
        public MenuType? MenuType { get; set; }

        public ICollection<MyEventMenu>? MyEventMenues { get; set; }
    }
}
