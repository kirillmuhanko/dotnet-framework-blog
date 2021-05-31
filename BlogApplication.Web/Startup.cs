using BlogApplication.Configuration;
using BlogApplication.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BlogApplication.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            StartupAuthConfig.ConfigureAuth(app);
            FileServerConfig.ConfigureApp(app);
        }
    }
}