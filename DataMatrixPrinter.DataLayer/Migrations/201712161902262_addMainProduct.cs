namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMainProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductMains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        LotProduct = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedIp = c.String(),
                        RowVersion = c.Binary(),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .Index(t => t.CreatedById);
            
            AddColumn("dbo.Products", "MainProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "MainProductID");
            AddForeignKey("dbo.Products", "MainProductID", "dbo.ProductMains", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "LOT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "LOT", c => c.String());
            DropForeignKey("dbo.Products", "MainProductID", "dbo.ProductMains");
            DropForeignKey("dbo.ProductMains", "CreatedById", "dbo.Users");
            DropIndex("dbo.ProductMains", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "MainProductID" });
            DropColumn("dbo.Products", "MainProductID");
            DropTable("dbo.ProductMains");
        }
    }
}
