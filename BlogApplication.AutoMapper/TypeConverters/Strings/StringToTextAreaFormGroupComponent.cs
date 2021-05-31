using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.Strings
{
    internal class StringToTextAreaFormGroupComponent : ITypeConverter<string, TextAreaFormGroupComponent>
    {
        public TextAreaFormGroupComponent Convert(
            string source,
            TextAreaFormGroupComponent destination,
            ResolutionContext context)
        {
            var component = new TextAreaFormGroupComponent {Text = source};
            return component;
        }
    }
}