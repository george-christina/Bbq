using System.ComponentModel.DataAnnotations;
using System;

namespace Bbq.Validations
{
    public class FutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime)value < DateTime.Now)
            {
                return new ValidationResult("Bbq must be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}