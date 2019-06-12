using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat_Hotel.Models
{
    public class ModelFormularRezervacija
    {
        public int RezervacijaID { get; set; }

        // [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Display(Name = "Br. dokumenta gosta")]
        public string GostID { get; set; }

        [Required(ErrorMessage = "ID Usluge obavezan")]
        public int UslugaID { get; set; }

        [Required(ErrorMessage = "Datum dolaska obavezan!")]
        [Display(Name = "Datum dolaska")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Check_InValidacijaDatuma]
        public System.DateTime Check_In { get; set; }

        [Required(ErrorMessage = "Datum odlaska obavezan!")]
        [Display(Name = "Datum odlaska")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [ValidacijaDatuma]
        public System.DateTime Check_Out { get; set; }

        [Required(ErrorMessage = "br sobe je obavezan!")]
        [Display(Name = "Broj sobe")]
        public int SobaID { get; set; }

        //public virtual Gost Gost { get; set; }
        
        //public virtual ICollection<Racun> Racuns { get; set; }
        //public virtual Soba Soba { get; set; }
        //public virtual Usluga Usluga { get; set; }
        [Required(ErrorMessage = "Ime je obavezno!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno!")]
        public string Prezime { get; set; }
        [RegularExpression("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}", ErrorMessage = "Email nije u ispravnom formatu!")]
        public string Email { get; set; }
    }
}