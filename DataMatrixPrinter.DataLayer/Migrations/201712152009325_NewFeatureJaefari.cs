namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFeatureJaefari : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LOT", c => c.String());
            DropColumn("dbo.Products", "Gs1ItemCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Gs1ItemCode", c => c.String());
            DropColumn("dbo.Products", "LOT");
        }
    }
}
