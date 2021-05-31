using System.Web;
using BlogApplication.Shared.Interfaces.Converters;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Shared.Converters
{
    public class ByteArrayConverter : IByteArrayConverter
    {
        private readonly IStreamFacade _streamFacade;

        public ByteArrayConverter(IStreamFacade streamFacade)
        {
            _streamFacade = streamFacade;
        }

        public byte[] ConvertFrom(HttpPostedFileBase fileBase)
        {
            if (fileBase == null) return new byte[0];

            var bytes = new byte[fileBase.InputStream.Length];
            _streamFacade.Read(fileBase.InputStream, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}