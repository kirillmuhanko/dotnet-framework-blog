using System.IO;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Shared.Facades
{
    public class StreamFacade : IStreamFacade
    {
        public int Read(Stream stream, byte[] buffer, int offset, int count)
        {
            var result = stream.Read(buffer, offset, count);
            return result;
        }
    }
}