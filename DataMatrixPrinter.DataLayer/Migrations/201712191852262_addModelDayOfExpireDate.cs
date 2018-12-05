namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModelDayOfExpireDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ExpireDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "EneteredDays", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Price", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "EneteredDays");
            DropColumn("dbo.Products", "ExpireDate");
        }
    }
}
