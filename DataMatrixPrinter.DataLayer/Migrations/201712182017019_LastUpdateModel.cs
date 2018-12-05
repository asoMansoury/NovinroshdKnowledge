namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastUpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserComapny", "RoleID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserComapny", "RoleID");
            AddForeignKey("dbo.UserComapny", "RoleID", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserComapny", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserComapny", new[] { "RoleID" });
            DropColumn("dbo.UserComapny", "RoleID");
        }
    }
}
