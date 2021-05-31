using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.Strings
{
    internal class StringToUserNameFormGroupComponent : ITypeConverter<string, UserNameFormGroupComponent>
    {
        public UserNameFormGroupComponent Convert(
            string source,
            UserNameFormGroupComponent destination,
            ResolutionContext context)
        {
            var component = new UserNameFormGroupComponent {Text = source};
            return component;
        }
    }
}