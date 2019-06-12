

namespace Projekat_Hotel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Radnik
    {
        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        [RegularExpression("[0-9]{13}", ErrorMessage = "JMBG se sastoji od 13 cifara")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        public string Prezime { get; set; }

        public string Adresa { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        [Display(Name = "Datum zaposlenja")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DatumZaposlenja { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!!!")]
        [Display(Name = "Radno mesto")]
        public string RadnoMesto { get; set; }

        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }


        public string Lozinka { get; set; }

        [Display(Name = "Privilegije")]
        public Nullable<int> UlogaID { get; set; }

        [RegularExpression("(\\+[0-9]{1,3})?[0-9]{9,10}$", ErrorMessage = "Broj telefona nije u ispravnom formatu")]
        [MaxLength(13, ErrorMessage = "Maksimalnaduzina broja je 13 cifara")]
        public string Telefon { get; set; }


        public string LogingErrorMessage { get; set; }

        public virtual Uloga Uloga { get; set; }
    }
}
