namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationCountryCompany2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "CountryID", newName: "Country_ID");
            RenameIndex(table: "dbo.Companies", name: "IX_CountryID", newName: "IX_Country_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Companies", name: "IX_Country_ID", newName: "IX_CountryID");
            RenameColumn(table: "dbo.Companies", name: "Country_ID", newName: "CountryID");
        }
    }
}
