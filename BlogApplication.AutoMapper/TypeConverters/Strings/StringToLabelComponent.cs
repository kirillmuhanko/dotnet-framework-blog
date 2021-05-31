using AutoMapper;
using BlogApplication.Models.Components.Controls;

namespace BlogApplication.AutoMapper.TypeConverters.Strings
{
    internal class StringToLabelComponent : ITypeConverter<string, LabelComponent>
    {
        public LabelComponent Convert(string source, LabelComponent destination, ResolutionContext context)
        {
            var component = new LabelComponent {Text = source};
            return component;
        }
    }
}