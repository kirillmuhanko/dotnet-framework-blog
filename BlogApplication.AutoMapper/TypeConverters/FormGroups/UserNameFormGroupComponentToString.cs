using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class UserNameFormGroupComponentToString : ITypeConverter<UserNameFormGroupComponent, string>
    {
        public string Convert(UserNameFormGroupComponent source, string destination, ResolutionContext context)
        {
            var str = source?.Text;
            return str;
        }
    }
}