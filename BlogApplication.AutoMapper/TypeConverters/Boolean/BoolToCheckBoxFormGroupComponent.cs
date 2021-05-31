using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.Boolean
{
    internal class BoolToCheckBoxFormGroupComponent : ITypeConverter<bool, CheckBoxFormGroupComponent>
    {
        public CheckBoxFormGroupComponent Convert(
            bool source,
            CheckBoxFormGroupComponent destination,
            ResolutionContext context)
        {
            var component = new CheckBoxFormGroupComponent {IsChecked = source};
            return component;
        }
    }
}