using System.Web;
using BlogApplication.Domain.Interfaces.Providers;
using Microsoft.AspNet.Identity;

namespace BlogApplication.Infrastructure.Implementations.Domain.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public string UserId => $"{HttpContext.Current?.User?.Identity?.GetUserId()}";
    }
}