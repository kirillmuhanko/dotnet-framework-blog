using System.Web;
using System.Web.Mvc;
using Autofac;
using BlogApplication.Configuration;
using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.IntegrationTests.Builders.Web;
using BlogApplication.IntegrationTests.Mocks.Web;
using BlogApplication.Web;

namespace BlogApplication.IntegrationTests.Base
{
    public class IntegrationTestBase
    {
        private static IDependencyResolver _dependencyResolver;

        protected static T GetService<T>()
        {
            var service = _dependencyResolver.GetService<T>();
            return service;
        }

        protected static void SetUpDependencyInjection()
        {
            if (_dependencyResolver != null) return;
            var containerBuilder = AutofacConfig.RegisterApp<MvcApplication>();
            RegisterMocks(containerBuilder);
            AutofacConfig.Build(containerBuilder);
            _dependencyResolver = DependencyResolver.Current;
            var httpContextBuilder = new HttpContextBuilder();
            HttpContext.Current = httpContextBuilder.GetHttpContext();
        }

        private static void RegisterMocks(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<HttpContextProviderMock>().As<IHttpContextProvider>().SingleInstance();
        }
    }
}