namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesJaefari : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductMains", "CompanyID", c => c.Int());
            CreateIndex("dbo.ProductMains", "CompanyID");
            AddForeignKey("dbo.ProductMains", "CompanyID", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMains", "CompanyID", "dbo.Companies");
            DropIndex("dbo.ProductMains", new[] { "CompanyID" });
            DropColumn("dbo.ProductMains", "CompanyID");
        }
    }
}
