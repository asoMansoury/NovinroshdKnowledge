namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserRoleUserdelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserRoles", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "ApplicationUser_Id", c => c.Int());
            CreateIndex("dbo.UserRoles", "ApplicationUser_Id");
            AddForeignKey("dbo.UserRoles", "ApplicationUser_Id", "dbo.Users", "Id");
        }
    }
}
