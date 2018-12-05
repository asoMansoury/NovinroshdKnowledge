using System.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using DataMatrixPrinter.DataLayer;
using DataMatrixPrinter.IocConfig;
using MobinYarCrm;


namespace DataMatrixPrinter
{
    public static class ApplicationStart
    {
        public static void Config()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            //ModelValidatorProviders.Providers.Clear();
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //ModelValidatorProviders.Providers.Add(new DataAnnotationsModelMetadataValidatorProvider());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataLayer.Context.ApplicationDbContext, DataLayer.Migrations.ConfigurationMigration>());
            //ProjectObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}