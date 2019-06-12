using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat_Hotel
{
    public class ValidacijaDatuma : ValidationAttribute
    {

        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.ModelFormularRezervacija)validationContext.ObjectInstance;
            DateTime Check_out = Convert.ToDateTime(model.Check_Out);
            DateTime Check_in = Convert.ToDateTime(model.Check_In);

            if (Check_in > Check_out)
            {
                return new ValidationResult
                    ("Datum odjave mora da bude posle datuma prijave.");
            }
           
            else
            {
                return ValidationResult.Success;
            }
        }


    }
}