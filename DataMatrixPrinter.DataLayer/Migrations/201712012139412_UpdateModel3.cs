namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Country_Id", c => c.Int());
            CreateIndex("dbo.Companies", "Country_Id");
            AddForeignKey("dbo.Companies", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Companies", new[] { "Country_Id" });
            DropColumn("dbo.Companies", "Country_Id");
        }
    }
}
