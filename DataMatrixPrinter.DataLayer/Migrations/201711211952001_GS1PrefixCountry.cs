namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GS1PrefixCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "GS1Prefix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "GS1Prefix");
        }
    }
}
