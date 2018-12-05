namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatePickerExpire : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "EneteredDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "EneteredDays", c => c.Int(nullable: false));
        }
    }
}
