using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class TextBoxFormGroupComponentToString : ITypeConverter<TextBoxFormGroupComponent, string>
    {
        public string Convert(TextBoxFormGroupComponent source, string destination, ResolutionContext context)
        {
            var str = source?.Text;
            return str;
        }
    }
}