namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationCompanyUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecondaryLbls", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Customers", "CreatedById", "dbo.Users");
            DropIndex("dbo.Customers", new[] { "CreatedById" });
            DropIndex("dbo.Cities", new[] { "CountryID" });
            DropIndex("dbo.SecondaryLbls", new[] { "CreatedById" });
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.CompanyID })
                .ForeignKey("dbo.Companies", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CompanyID);
            
            AlterColumn("dbo.Cities", "CityCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Cities", "CountryId");
            DropTable("dbo.Customers");
            DropTable("dbo.SecondaryLbls");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SecondaryLbls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        GTIN = c.String(),
                        GS1ItemCode = c.String(),
                        CompanyPrefix = c.String(),
                        PackType = c.String(),
                        QtyPack = c.Long(nullable: false),
                        UOM = c.String(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Batch = c.String(),
                        ExpDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.CompanyUsers", "CompanyID", "dbo.Users");
            DropForeignKey("dbo.CompanyUsers", "UserID", "dbo.Companies");
            DropIndex("dbo.CompanyUsers", new[] { "CompanyID" });
            DropIndex("dbo.CompanyUsers", new[] { "UserID" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            AlterColumn("dbo.Cities", "CityName", c => c.String());
            AlterColumn("dbo.Cities", "CityCode", c => c.String());
            DropTable("dbo.CompanyUsers");
            CreateIndex("dbo.SecondaryLbls", "CreatedById");
            CreateIndex("dbo.Cities", "CountryID");
            CreateIndex("dbo.Customers", "CreatedById");
            AddForeignKey("dbo.Customers", "CreatedById", "dbo.Users", "Id");
            AddForeignKey("dbo.SecondaryLbls", "CreatedById", "dbo.Users", "Id");
        }
    }
}
