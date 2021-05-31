using System.Web.Mvc;
using System.Web.Routing;
using BlogApplication.Models.Constants.Controllers;

namespace BlogApplication.Configuration
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes(); // Enable attribute routing.

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    action = ActionNames.Index,
                    controller = ControllerNames.Home,
                    id = UrlParameter.Optional
                }
            );
        }
    }
}