using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;


namespace DataMatrixPrinter.DataLayer.Migrations
{

    public sealed class ConfigurationMigration : DbMigrationsConfiguration<DataMatrixPrinter.DataLayer.Context.ApplicationDbContext>
    {
        public ConfigurationMigration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataMatrixPrinter.DataLayer.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
