using System.IO;
using System.Runtime.InteropServices;

namespace BlogApplication.Shared.Interfaces.Facades
{
    public interface IStreamFacade
    {
        int Read([In] [Out] Stream stream, byte[] buffer, int offset, int count);
    }
}