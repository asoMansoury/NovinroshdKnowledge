namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoleControllers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionID = c.Int(),
                        RoleID = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(),
                        RowVersion = c.Binary(),
                        CreatedById = c.Int(nullable: false),
                        Roles_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActionMethods", t => t.ActionID)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .Index(t => t.ActionID)
                .Index(t => t.CreatedById)
                .Index(t => t.Roles_Id);
            
            CreateTable(
                "dbo.ActionMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                        ControllerID = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(),
                        RowVersion = c.Binary(),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controllers", t => t.ControllerID)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .Index(t => t.ControllerID)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Controllers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(),
                        RowVersion = c.Binary(),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.ControllerUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerID = c.Int(),
                        RoleID = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(),
                        RowVersion = c.Binary(),
                        CreatedById = c.Int(nullable: false),
                        Roles_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controllers", t => t.ControllerID)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .Index(t => t.ControllerID)
                .Index(t => t.CreatedById)
                .Index(t => t.Roles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActionUsers", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.ActionUsers", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ActionMethods", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ActionMethods", "ControllerID", "dbo.Controllers");
            DropForeignKey("dbo.Controllers", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ControllerUsers", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.ControllerUsers", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ControllerUsers", "ControllerID", "dbo.Controllers");
            DropForeignKey("dbo.ActionUsers", "ActionID", "dbo.ActionMethods");
            DropIndex("dbo.ControllerUsers", new[] { "Roles_Id" });
            DropIndex("dbo.ControllerUsers", new[] { "CreatedById" });
            DropIndex("dbo.ControllerUsers", new[] { "ControllerID" });
            DropIndex("dbo.Controllers", new[] { "CreatedById" });
            DropIndex("dbo.ActionMethods", new[] { "CreatedById" });
            DropIndex("dbo.ActionMethods", new[] { "ControllerID" });
            DropIndex("dbo.ActionUsers", new[] { "Roles_Id" });
            DropIndex("dbo.ActionUsers", new[] { "CreatedById" });
            DropIndex("dbo.ActionUsers", new[] { "ActionID" });
            DropTable("dbo.ControllerUsers");
            DropTable("dbo.Controllers");
            DropTable("dbo.ActionMethods");
            DropTable("dbo.ActionUsers");
        }
    }
}
