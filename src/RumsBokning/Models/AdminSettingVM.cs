using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class AdminSettingVM
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "E-post")]
        [Required(ErrorMessage = "Fyll i e-post")]
        public string UserName { get; set; }

        //[Display(Name = "Lösenord")]
        //[Required(ErrorMessage = "Fyll i lösenord")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Lägg till förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Lägg till efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Kontotyp")]
        [Required(ErrorMessage = "Lägg till kontotyp")]
        public string Category { get; set; }
    }
}
