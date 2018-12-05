namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "State", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "State", c => c.Int(nullable: false));
        }
    }
}
