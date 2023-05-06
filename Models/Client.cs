using EventPlannerApp.Models.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EventPlannerApp.Models
{
    public class Client 
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Your first name must start with capital letter(ex.Ana or Ana Maria or Ana - Maria")]
        [StringLength(30, MinimumLength = 3)]

        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Your last name must start with capital letter(ex.Pop or Popovici")]
        [StringLength(30, MinimumLength = 3)]

        public string? LastName { get; set; }
        [StringLength(70)]

        public string? Adress { get; set; }
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,3}$", ErrorMessage = "Your email must be like 'mari@gmail.com' or 'sonia@yahoo.com'")]
        public string Email { get; set; }
        [RegularExpression(@"^([0]{1})[-. ]?([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
         ErrorMessage = "Your phone number must start with '0'")]
        public string? Phone { get; set; }
        [Display(Name = "Full Name: ")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<MyEvent>? MyEvents { get; set; }

    }
}
