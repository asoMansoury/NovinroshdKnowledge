namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertAddressToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Address1", c => c.String());
            AddColumn("dbo.Companies", "Address2", c => c.String());
            AddColumn("dbo.Companies", "Address3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Address3");
            DropColumn("dbo.Companies", "Address2");
            DropColumn("dbo.Companies", "Address1");
        }
    }
}
