using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class PasswordFormGroupComponentToString : ITypeConverter<PasswordFormGroupComponent, string>
    {
        public string Convert(PasswordFormGroupComponent source, string destination, ResolutionContext context)
        {
            var str = source?.Text;
            return str;
        }
    }
}