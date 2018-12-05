namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportPrintedProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isPrinted", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "TypeOfPrinted", c => c.String());
            DropColumn("dbo.Products", "CountOfProduct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CountOfProduct", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "TypeOfPrinted");
            DropColumn("dbo.Products", "isPrinted");
        }
    }
}
