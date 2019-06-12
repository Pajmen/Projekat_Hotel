using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Projekat_Hotel.Models;

namespace Projekat_Hotel
{
    public class Check_InValidacijaDatuma : ValidationAttribute
    {
        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            var rezervacija = (ModelFormularRezervacija)validationContext.ObjectInstance;
            DateTime Check_in = rezervacija.Check_In;
            DateTime danas = DateTime.Now;
            if ((Check_in.Date==danas.Date )|| Check_in>danas)
            {
                return ValidationResult.Success;
            }
            if (Check_in == null)
            {
                return new ValidationResult
                   ("Ovo  polje je obavezno!");
            }
            else
            {
                return new ValidationResult
                    ("Datum mora da bude u buducnosti");
            }
        }
    }
}