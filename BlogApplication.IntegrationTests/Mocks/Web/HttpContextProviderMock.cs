using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.IntegrationTests.Models.Web;

namespace BlogApplication.IntegrationTests.Mocks.Web
{
    public class HttpContextProviderMock : IHttpContextProvider
    {
        public string UserId => UserContextModel.UserId;
    }
}