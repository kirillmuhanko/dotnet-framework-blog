using System.IO;
using System.Security.Principal;
using System.Web;

namespace BlogApplication.IntegrationTests.Builders.Web
{
    internal class HttpContextBuilder
    {
        private const string FakeUrl = "http://localhost";
        private HttpContext _httpContext;

        public HttpContextBuilder()
        {
            Reset();
        }

        public HttpContext GetHttpContext()
        {
            var httpContext = _httpContext;
            Reset();
            return httpContext;
        }

        public void BuildLoggedInUser(string username)
        {
            _httpContext.User = new GenericPrincipal(
                new GenericIdentity(username),
                new string[0]
            );
        }

        public void BuildLoggedOutUser()
        {
            _httpContext.User = new GenericPrincipal(
                new GenericIdentity(string.Empty),
                new string[0]
            );
        }

        private void Reset()
        {
            _httpContext = new HttpContext(
                new HttpRequest(null, FakeUrl, null),
                new HttpResponse(new StringWriter())
            );
        }
    }
}