using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.Attributes.FormGroups
{
    public class MaxLengthFormGroupAttribute : MaxLengthAttribute
    {
        public MaxLengthFormGroupAttribute(int length) : base(length)
        {
        }

        public override bool IsValid(object value)
        {
            if (value is TextFormGroupComponentBase component)
            {
                var isValid = base.IsValid(component.Text);
                component.SetValidationState(isValid);
                return isValid;
            }

            return true;
        }
    }
}