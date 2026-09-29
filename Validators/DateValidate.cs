using CollageApi.Data;
using System.ComponentModel.DataAnnotations;

namespace CollageApi.Validators
{
    public class DateValidate:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var a= (DateTime)value;
           
            
            if (a.Date <= DateTime.Today)
                
            {
                return new ValidationResult($"date is invalid: must be after todays date.");
            }
            
                return ValidationResult.Success;
            
        }
    }
}
