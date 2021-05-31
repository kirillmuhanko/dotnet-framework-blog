using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class CheckBoxFormGroupComponentToBool : ITypeConverter<CheckBoxFormGroupComponent, bool>
    {
        public bool Convert(CheckBoxFormGroupComponent source, bool destination, ResolutionContext context)
        {
            var isChecked = source?.IsChecked ?? false;
            return isChecked;
        }
    }
}