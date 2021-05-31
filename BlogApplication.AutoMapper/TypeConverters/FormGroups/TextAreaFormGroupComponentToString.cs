using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class TextAreaFormGroupComponentToString : ITypeConverter<TextAreaFormGroupComponent, string>
    {
        public string Convert(TextAreaFormGroupComponent source, string destination, ResolutionContext context)
        {
            var str = source?.Text;
            return str;
        }
    }
}