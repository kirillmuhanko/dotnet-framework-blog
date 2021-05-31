using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.Strings
{
    internal class StringToTextBoxFormGroupComponent : ITypeConverter<string, TextBoxFormGroupComponent>
    {
        public TextBoxFormGroupComponent Convert(
            string source,
            TextBoxFormGroupComponent destination,
            ResolutionContext context)
        {
            var component = new TextBoxFormGroupComponent {Text = source};
            return component;
        }
    }
}