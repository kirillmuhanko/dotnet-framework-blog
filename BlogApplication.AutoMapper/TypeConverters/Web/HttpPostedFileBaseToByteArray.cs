using System.Web;
using AutoMapper;
using BlogApplication.Shared.Interfaces.Converters;

namespace BlogApplication.AutoMapper.TypeConverters.Web
{
    internal class HttpPostedFileBaseToByteArray : ITypeConverter<HttpPostedFileBase, byte[]>
    {
        private readonly IByteArrayConverter _byteArrayConverter;

        public HttpPostedFileBaseToByteArray(IByteArrayConverter byteArrayConverter)
        {
            _byteArrayConverter = byteArrayConverter;
        }

        public byte[] Convert(HttpPostedFileBase source, byte[] destination, ResolutionContext context)
        {
            var bytes = _byteArrayConverter.ConvertFrom(source);
            return bytes;
        }
    }
}