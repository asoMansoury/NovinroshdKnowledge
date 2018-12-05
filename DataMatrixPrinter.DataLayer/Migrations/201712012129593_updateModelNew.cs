namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserComapny", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Companies", "CountryTBl_Id", "dbo.Countries");
            DropIndex("dbo.UserComapny", new[] { "Country_Id" });
            DropIndex("dbo.Companies", new[] { "CountryTBl_Id" });
            DropColumn("dbo.UserComapny", "Country_Id");
            DropColumn("dbo.Companies", "CountryTBl_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "CountryTBl_Id", c => c.Int());
            AddColumn("dbo.UserComapny", "Country_Id", c => c.Int());
            CreateIndex("dbo.Companies", "CountryTBl_Id");
            CreateIndex("dbo.UserComapny", "Country_Id");
            AddForeignKey("dbo.Companies", "CountryTBl_Id", "dbo.Countries", "Id");
            AddForeignKey("dbo.UserComapny", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
