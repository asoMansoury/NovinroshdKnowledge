namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GTINModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductMains", "Serial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductMains", "Serial");
        }
    }
}
