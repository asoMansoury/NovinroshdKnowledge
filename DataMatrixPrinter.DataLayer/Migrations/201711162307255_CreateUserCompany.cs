namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompanyUsers", "UserID", "dbo.Companies");
            DropForeignKey("dbo.CompanyUsers", "CompanyID", "dbo.Users");
            DropForeignKey("dbo.Companies", "CreatedById", "dbo.Users");
            DropIndex("dbo.CompanyUsers", new[] { "UserID" });
            DropIndex("dbo.CompanyUsers", new[] { "CompanyID" });
            CreateTable(
                "dbo.UserComapny",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.UserID)
                .Index(t => t.CompanyID)
                .Index(t => t.CreatedById)
                .Index(t => t.Country_Id);
            
            AddForeignKey("dbo.Companies", "CreatedById", "dbo.Users", "Id", cascadeDelete: true);
            DropTable("dbo.CompanyUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.CompanyID });
            
            DropForeignKey("dbo.Companies", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserComapny", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.UserComapny", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserComapny", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserComapny", "CompanyID", "dbo.Companies");
            DropIndex("dbo.UserComapny", new[] { "Country_Id" });
            DropIndex("dbo.UserComapny", new[] { "CreatedById" });
            DropIndex("dbo.UserComapny", new[] { "CompanyID" });
            DropIndex("dbo.UserComapny", new[] { "UserID" });
            DropTable("dbo.UserComapny");
            CreateIndex("dbo.CompanyUsers", "CompanyID");
            CreateIndex("dbo.CompanyUsers", "UserID");
            AddForeignKey("dbo.Companies", "CreatedById", "dbo.Users", "Id");
            AddForeignKey("dbo.CompanyUsers", "CompanyID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyUsers", "UserID", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
