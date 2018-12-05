namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityCode = c.String(),
                        CityName = c.String(),
                        CountryID = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.CountryID)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        GS1Company = c.String(),
                        CompanyName = c.String(),
                        State = c.Int(nullable: false),
                        PhoneNos = c.String(),
                        WebSite = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        Description = c.String(),
                        CountryCode = c.String(),
                        PackingType = c.String(),
                        GS1ItemCode = c.String(),
                        GTIN = c.String(),
                        QtyPerPack = c.Long(nullable: false),
                        UOM = c.String(),
                        WeightPerPack = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Templet = c.String(),
                        CompanyCode = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyCode)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.CompanyCode)
                .Index(t => t.CreatedById);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecondaryLbls", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Products", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Products", "CompanyCode", "dbo.Companies");
            DropForeignKey("dbo.Companies", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Cities", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Cities", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.Users");
            DropIndex("dbo.SecondaryLbls", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "CompanyCode" });
            DropIndex("dbo.Companies", new[] { "CreatedById" });
            DropIndex("dbo.Countries", new[] { "CreatedById" });
            DropIndex("dbo.Cities", new[] { "CreatedById" });
            DropIndex("dbo.Cities", new[] { "CountryID" });
            DropTable("dbo.SecondaryLbls");
            DropTable("dbo.Products");
            DropTable("dbo.Companies");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
