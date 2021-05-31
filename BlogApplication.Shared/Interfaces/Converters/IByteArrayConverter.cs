using System.Web;

namespace BlogApplication.Shared.Interfaces.Converters
{
    public interface IByteArrayConverter
    {
        byte[] ConvertFrom(HttpPostedFileBase fileBase);
    }
}