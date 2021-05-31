using System.ComponentModel.DataAnnotations;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.Models.Attributes.FormGroups
{
    public class RequiredFormGroupAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is CheckBoxFormGroupComponent checkBoxComponent)
            {
                var isChecked = checkBoxComponent.IsChecked;
                checkBoxComponent.SetValidationState(isChecked);
                return isChecked;
            }

            if (value is ImageFormGroupComponent imageComponent)
            {
                var isValid = base.IsValid(imageComponent.Image);
                imageComponent.SetValidationState(isValid);
                return isValid;
            }

            if (value is TextFormGroupComponentBase textComponentBase)
            {
                var isValid = base.IsValid(textComponentBase.Text);
                textComponentBase.SetValidationState(isValid);
                return isValid;
            }

            return true;
        }
    }
}