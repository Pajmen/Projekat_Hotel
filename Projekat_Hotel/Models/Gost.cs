
namespace Projekat_Hotel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Gost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gost()
        {
            this.Rezervacijas = new HashSet<Rezervacija>();
        }
        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        [Display(Name = "Br. lične isprave")]
        public string GostID { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        public string Prezime { get; set; }

        
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezervacija> Rezervacijas { get; set; }
    }
}
