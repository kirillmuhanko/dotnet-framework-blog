using BlogApplication.Models.Constants.Security;

namespace BlogApplication.IntegrationTests.Models.Web
{
    public static class UserContextModel
    {
        public static string UserId { get; set; } = UserIds.Admin;
    }
}