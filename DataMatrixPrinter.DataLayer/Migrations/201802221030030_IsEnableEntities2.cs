namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEnableEntities2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isEnable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isEnable");
        }
    }
}
