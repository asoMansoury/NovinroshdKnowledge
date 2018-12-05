namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "CreatedById", "dbo.Users");
            AddForeignKey("dbo.Companies", "CreatedById", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "CreatedById", "dbo.Users");
            AddForeignKey("dbo.Companies", "CreatedById", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
