using System.Collections.Generic;
using System.Reflection;
using BlogApplication.AutoMapper.IoC;
using BlogApplication.Domain.IoC;
using BlogApplication.Infrastructure.IoC;
using BlogApplication.Shared.IoC;

namespace BlogApplication.Configuration.Constants
{
    internal static class AssemblyConstants
    {
        public static readonly IEnumerable<Assembly> Projects = new List<Assembly>
        {
            typeof(AutoMapperModule).Assembly,
            typeof(DomainModule).Assembly,
            typeof(InfrastructureModule).Assembly,
            typeof(SharedModule).Assembly
        };
    }
}