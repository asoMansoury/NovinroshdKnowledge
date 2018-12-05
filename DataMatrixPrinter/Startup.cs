using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin;
using Owin;
using StructureMap.Web;
using System;
using DataMatrixPrinter.IocConfig;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.EfServices.Identites;
using System.Web.Mvc;

namespace DataMatrixPrinter
{
    //[assembly: OwinStartupAttribute(typeof(AspNetIdentityDependencyInjectionSample.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            var container = ProjectObjectFactory.Container;
            
            container.Configure(config =>
            {
                config.For<IDataProtectionProvider>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use(() => app.GetDataProtectionProvider());
            });

            // This is necessary for `GenerateUserIdentityAsync` and `SecurityStampValidator` to work internally by ASP.NET Identity 2.x
            app.CreatePerOwinContext(() => (UserService)container.GetInstance<IUserService>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Login"),
                CookieName = ".DataMatrixPrinter",
                ExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    //OnValidateIdentity = container.GetInstance<IUserService>().OnValidateIdentity()
                },
                SlidingExpiration = false,
                //CookieManager = new SystemWebCookieManager()
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.MapSignalR();
            //DependencyResolver.SetResolver(container);
        }
    }
}