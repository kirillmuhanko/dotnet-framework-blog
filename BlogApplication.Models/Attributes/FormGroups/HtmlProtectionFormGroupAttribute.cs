using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BlogApplication.Models.Attributes.FormGroups
{
    public class HtmlProtectionFormGroupAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string text)
                value = HttpUtility.HtmlEncode(text);

            return ValidationResult.Success;
        }
    }
}