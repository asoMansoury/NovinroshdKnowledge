namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCountryCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CountryTBl_Id", c => c.Int());
            CreateIndex("dbo.Companies", "CountryTBl_Id");
            AddForeignKey("dbo.Companies", "CountryTBl_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "CountryTBl_Id", "dbo.Countries");
            DropIndex("dbo.Companies", new[] { "CountryTBl_Id" });
            DropColumn("dbo.Companies", "CountryTBl_Id");
        }
    }
}
