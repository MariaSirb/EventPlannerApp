using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EventPlannerApp.Models.Services
{
    public class MenuType
    {
        public int ID { get; set; }
        [Display(Name = "Food category: ")]
        public string TypeName { get; set; }

        public ICollection<Menu>? Menues { get; set; }
    }
}
