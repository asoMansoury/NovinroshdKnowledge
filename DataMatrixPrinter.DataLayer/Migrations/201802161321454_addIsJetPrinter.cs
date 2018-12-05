namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsJetPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "isJetPrinter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "isJetPrinter");
        }
    }
}
