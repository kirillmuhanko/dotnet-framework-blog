using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlogApplication.Configuration;
using BlogApplication.Domain.Interfaces.Logging;

namespace BlogApplication.Web
{
    public abstract class MvcApplication : HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            var dependencyResolver = DependencyResolver.Current;
            var logger = dependencyResolver.GetService<ILogger>();
            var exception = Server.GetLastError();
            logger.Log(exception);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var containerBuilder = AutofacConfig.RegisterApp<MvcApplication>();
            AutofacConfig.Build(containerBuilder);
        }
    }
}