using AutoMapper;
using BlogApplication.Models.Components.FormGroups;

namespace BlogApplication.AutoMapper.TypeConverters.Strings
{
    internal class StringToPasswordFormGroupComponent : ITypeConverter<string, PasswordFormGroupComponent>
    {
        public PasswordFormGroupComponent Convert(
            string source,
            PasswordFormGroupComponent destination,
            ResolutionContext context)
        {
            var component = new PasswordFormGroupComponent {Text = source};
            return component;
        }
    }
}