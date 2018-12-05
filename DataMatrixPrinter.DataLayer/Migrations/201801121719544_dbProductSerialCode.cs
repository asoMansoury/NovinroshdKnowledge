namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbProductSerialCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SerialCode", c => c.String());
            DropColumn("dbo.Products", "ProductCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductCode", c => c.String());
            DropColumn("dbo.Products", "SerialCode");
        }
    }
}
