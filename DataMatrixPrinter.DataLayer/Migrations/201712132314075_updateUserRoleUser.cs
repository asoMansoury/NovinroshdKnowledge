namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserRoleUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRoles", "ApplicationUser_Id", c => c.Int());
            CreateIndex("dbo.UserRoles", "ApplicationUser_Id");
            AddForeignKey("dbo.UserRoles", "ApplicationUser_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserRoles", "ApplicationUser_Id");
        }
    }
}
