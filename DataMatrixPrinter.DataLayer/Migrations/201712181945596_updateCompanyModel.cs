namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCompanyModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UID");
        }
    }
}
