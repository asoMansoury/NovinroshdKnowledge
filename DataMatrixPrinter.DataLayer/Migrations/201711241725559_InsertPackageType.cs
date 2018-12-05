namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertPackageType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackageType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        PackageCount = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(nullable: false, maxLength: 45),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .Index(t => t.CreatedById);
            
            AddColumn("dbo.Products", "PackageTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "PackageTypeID");
            AddForeignKey("dbo.Products", "PackageTypeID", "dbo.PackageType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "PackageTypeID", "dbo.PackageType");
            DropForeignKey("dbo.PackageType", "CreatedById", "dbo.Users");
            DropIndex("dbo.PackageType", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "PackageTypeID" });
            DropColumn("dbo.Products", "PackageTypeID");
            DropTable("dbo.PackageType");
        }
    }
}
