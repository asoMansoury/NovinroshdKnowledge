namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationCountryCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Companies", new[] { "Country_Id" });
            RenameColumn(table: "dbo.Companies", name: "Country_Id", newName: "CountryID");
            AlterColumn("dbo.Companies", "CountryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Companies", "CountryID");
            AddForeignKey("dbo.Companies", "CountryID", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "CountryID", "dbo.Countries");
            DropIndex("dbo.Companies", new[] { "CountryID" });
            AlterColumn("dbo.Companies", "CountryID", c => c.Int());
            RenameColumn(table: "dbo.Companies", name: "CountryID", newName: "Country_Id");
            CreateIndex("dbo.Companies", "Country_Id");
            AddForeignKey("dbo.Companies", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
