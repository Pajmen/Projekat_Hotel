
namespace Projekat_Hotel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Soba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Soba()
        {
            this.Rezervacijas = new HashSet<Rezervacija>();
        }
        [Display(Name = "Broj sobe")]
        public int SobaID { get; set; }

        [Display(Name = "Broj kreveta")]
        public int BrojKreveta { get; set; }


        public string Pogled { get; set; }


        public int Sprat { get; set; }

        [Display(Name = "Status sobe")]
        public int StatusID { get; set; }


        public double Cena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezervacija> Rezervacijas { get; set; }
        public virtual StatusSobe StatusSobe { get; set; }
    }
}
