namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIDEntityTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Family", c => c.String());
            AddColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FullName", c => c.String());
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "Family");
        }
    }
}
