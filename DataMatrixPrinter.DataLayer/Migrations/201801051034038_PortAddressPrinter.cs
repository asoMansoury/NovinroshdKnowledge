namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PortAddressPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "IpAddress", c => c.String());
            AddColumn("dbo.Companies", "PortNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "PortNumber");
            DropColumn("dbo.Companies", "IpAddress");
        }
    }
}
