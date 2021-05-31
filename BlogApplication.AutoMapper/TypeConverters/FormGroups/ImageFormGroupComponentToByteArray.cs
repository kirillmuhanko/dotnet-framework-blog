using AutoMapper;
using BlogApplication.Models.Components.FormGroups;
using BlogApplication.Shared.Interfaces.Converters;

namespace BlogApplication.AutoMapper.TypeConverters.FormGroups
{
    internal class ImageFormGroupComponentToByteArray : ITypeConverter<ImageFormGroupComponent, byte[]>
    {
        private readonly IByteArrayConverter _byteArrayConverter;

        public ImageFormGroupComponentToByteArray(IByteArrayConverter byteArrayConverter)
        {
            _byteArrayConverter = byteArrayConverter;
        }

        public byte[] Convert(ImageFormGroupComponent source, byte[] destination, ResolutionContext context)
        {
            var bytes = _byteArrayConverter.ConvertFrom(source?.Image);
            return bytes;
        }
    }
}