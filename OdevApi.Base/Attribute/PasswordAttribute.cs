using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OdevApi.Base;

public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            string source = value.ToString();

            bool containsInt = source.Any(char.IsDigit);
            bool moreThanEigth = source.Length >= 8;

            if (!containsInt || !moreThanEigth)
                return new ValidationResult("Password must be longer than eight characters and must contain a number.");

            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult("Password must be longer than eight characters and must contain a number.");
        }
    }
}
