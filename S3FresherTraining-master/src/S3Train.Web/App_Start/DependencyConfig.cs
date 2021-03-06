﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using S3Train.Service;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using S3Train.Domain;

namespace S3Train.App_Start
{
    public static class DependencyConfig
    {
        public static IContainer RegisterDependencyResolvers()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterDependencyMappingDefaults(builder);
            RegisterDependencyMappingOverrides(builder);
            IContainer container = builder.Build();
            // Set Up MVC Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Set Up WebAPI Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        private static void RegisterDependencyMappingDefaults(ContainerBuilder builder)
        {
            Assembly coreAssembly = Assembly.GetAssembly(typeof(IStateManager));
            Assembly webAssembly = Assembly.GetAssembly(typeof(MvcApplication));

            builder.RegisterAssemblyTypes(coreAssembly).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterControllers(webAssembly);
            builder.RegisterModule(new AutofacWebTypesModule());
        }

        private static void RegisterDependencyMappingOverrides(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<ProductService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductAdvertisementService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductCategoryService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BrandService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AnswerService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<QuestionService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OrderService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OrderDetailService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FooterClientService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<MenuService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<MenuTypeService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ContentCategoryService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SpecService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SpecValueService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductSpecSpecValueService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OrderDetailTempService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}