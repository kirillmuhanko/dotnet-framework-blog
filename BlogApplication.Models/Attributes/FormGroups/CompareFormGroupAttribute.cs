using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.Attributes.FormGroups
{
    public class CompareFormGroupAttribute : ValidationAttribute
    {
        public CompareFormGroupAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        private string OtherProperty { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            var result = ValidationResult.Success;

            if (property?.PropertyType != typeof(PasswordFormGroupComponent)) return result;

            var component = property.GetValue(validationContext.ObjectInstance, null);

            if (value is TextFormGroupComponentBase textBase1 && component is TextFormGroupComponentBase textBase2)
            {
                var isValid = textBase1.Text == textBase2.Text;
                textBase1.SetValidationState(isValid);
                textBase2.SetValidationState(isValid);

                if (!isValid)
                {
                    result = new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                    return result;
                }
            }

            return result;
        }
    }
}