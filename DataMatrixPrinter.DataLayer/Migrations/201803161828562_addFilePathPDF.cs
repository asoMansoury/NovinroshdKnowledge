namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFilePathPDF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "FilePathPDF", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "FilePathPDF");
        }
    }
}
