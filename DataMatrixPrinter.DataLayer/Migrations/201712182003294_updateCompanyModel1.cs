namespace DataMatrixPrinter.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCompanyModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CompanyPrefix", c => c.String());
            AddColumn("dbo.Companies", "CompnayOptionalNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "CompnayOptionalNumber");
            DropColumn("dbo.Companies", "CompanyPrefix");
        }
    }
}
