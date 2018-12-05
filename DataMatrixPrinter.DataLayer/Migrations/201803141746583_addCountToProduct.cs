namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCountToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CountOfProduct", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CountOfProduct");
        }
    }
}
