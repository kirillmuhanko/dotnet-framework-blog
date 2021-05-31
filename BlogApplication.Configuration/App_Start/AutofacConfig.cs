using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BlogApplication.AutoMapper.IoC;
using BlogApplication.Configuration.Constants;
using BlogApplication.Domain.Builders.Queries;
using BlogApplication.Domain.Expressions.Common;
using BlogApplication.Domain.Interfaces.Builders.Queries;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Common;
using BlogApplication.Domain.Interfaces.Managers.Base;
using BlogApplication.Domain.Interfaces.Services.Common;
using BlogApplication.Domain.Managers.Base;
using BlogApplication.Domain.Services.Common;
using BlogApplication.Infrastructure.Database;
using BlogApplication.Infrastructure.Implementations.Domain.Data.Entity;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Enums;

namespace BlogApplication.Configuration
{
    public static class AutofacConfig
    {
        public static ContainerBuilder RegisterApp<TMvcApplication>()
        {
            var builder = new ContainerBuilder();
            var types = AssemblyConstants.Projects.SelectMany(t => t.GetTypes()).ToList();
            var mvcAssembly = GetAssembly<TMvcApplication>();
            types.AddRange(mvcAssembly.GetTypes());

            ICollection<Type> contracts = new Collection<Type>();
            ICollection<Type> implementations = new Collection<Type>();

            foreach (var type in types)
            {
                if (type.IsInterface) contracts.Add(type);
                if (type.IsClass && !type.IsAbstract) implementations.Add(type);
            }

            RegisterTypes(builder, contracts, implementations);
            RegisterGenericTypes(builder);
            builder.RegisterAutoMapper(GetAssembly<AutoMapperModule>());
            builder.RegisterControllers(mvcAssembly);
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterType<BlogDbContext>().InstancePerRequest();
            return builder;
        }

        public static void Build(ContainerBuilder containerBuilder)
        {
            var container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static Assembly GetAssembly<T>()
        {
            var assembly = typeof(T).Assembly;
            return assembly;
        }

        private static void RegisterGenericTypes(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EntityExpressions<>)).As(typeof(IEntityExpressions<>));
            builder.RegisterGeneric(typeof(EntityManagerBase<>)).As(typeof(IEntityManagerBase<>));
            builder.RegisterGeneric(typeof(EntityQueryBuilder<>)).As(typeof(IEntityQueryBuilder<>));
            builder.RegisterGeneric(typeof(EntityService<>)).As(typeof(IEntityService<>));
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }

        private static void RegisterTypes(
            ContainerBuilder builder,
            ICollection<Type> contracts,
            IEnumerable<Type> implementations)
        {
            foreach (var implementation in implementations)
            {
                var contract = contracts.FirstOrDefault(t => implementation.GetInterfaces().Contains(t));
                if (contract == null) continue;
                var attribute = Attribute.GetCustomAttribute(implementation, typeof(InstanceScopeAttribute));
                var instanceScope = attribute as InstanceScopeAttribute;
                var registrationBuilder = builder.RegisterType(implementation).As(contract);

                if (instanceScope == null)
                {
                    registrationBuilder.SingleInstance();
                    continue;
                }

                switch (instanceScope.Lifetime)
                {
                    case InstanceLifetime.PerDependency:
                        registrationBuilder.InstancePerDependency();
                        break;
                    case InstanceLifetime.PerLifeTimeScope:
                        registrationBuilder.InstancePerLifetimeScope();
                        break;
                    case InstanceLifetime.PerRequest:
                        registrationBuilder.InstancePerRequest();
                        break;
                    case InstanceLifetime.Singleton:
                        registrationBuilder.SingleInstance();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}