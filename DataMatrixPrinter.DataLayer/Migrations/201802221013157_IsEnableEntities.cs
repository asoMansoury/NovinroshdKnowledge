namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEnableEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "isEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Companies", "isEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductMains", "isEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "isEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.PackageType", "isEnable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PackageType", "isEnable");
            DropColumn("dbo.Products", "isEnable");
            DropColumn("dbo.ProductMains", "isEnable");
            DropColumn("dbo.Companies", "isEnable");
            DropColumn("dbo.Countries", "isEnable");
        }
    }
}
