using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using AutoMapper;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.EfServices.Business;
using DataMatrixPrinter.ServiceLayer.EfServices.Identites;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using StructureMap;
using StructureMap.Web;

namespace DataMatrixPrinter.IocConfig
{
    public class ProjectObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container => ContainerBuilder.Value;

        private static Container DefaultContainer()
        {
            var defaultContainer = new Container(ioc =>
            {

                ioc.For<IIdentity>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(() => GetIdentity());

                ioc.For<IActionService>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ActionService>();

                ioc.For<IUnitOfWork>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<ApplicationDbContext>();

                ioc.For<ApplicationDbContext>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
                ioc.For<DbContext>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

                ioc.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<UserStoreService>();

                ioc.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleStoreService>();

                ioc.For<IActionUserService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<ActionUserService>();

                ioc.For<IAuthenticationManager>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                ioc.For<ISignInService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<SignInService>();

                ioc.For<IRoleService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleService>();

                ioc.For<ICountryService>()
                     .HybridHttpOrThreadLocalScoped()
                     .Use<CountryService>();
                ioc.For<ICityService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CityService>();

                ioc.For<IIdentityMessageService>().Use<SmsService>();
                ioc.For<IIdentityMessageService>().Use<EmailService>();

                ioc.For<IUserService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<UserService>()
                    .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                    .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                    .Setter(userManager => userManager.SmsService).Is<SmsService>()
                    .Setter(userManager => userManager.EmailService).Is<EmailService>();

                ioc.For<UserService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(context => (UserService)context.GetInstance<IUserService>());

                ioc.For<IRoleStoreService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleStoreService>();

                ioc.For<IUserStoreService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<UserStoreService>();

                ioc.AddRegistry<AutoMapperRegistery>();



            });

            ConfigureAutoMapper(defaultContainer);
            
            return defaultContainer;
        }
        private static void ConfigureAutoMapper(IContainer container)
        {
            var configuration = container.TryGetInstance<IConfiguration>();
            if (configuration == null) return;
            //saying AutoMapper how to resolve services
            configuration.ConstructServicesUsing(container.GetInstance);
            foreach (var profile in container.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }
        }
        private static IIdentity GetIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }

        private class DependencyResolver
        {
        }
    }
}