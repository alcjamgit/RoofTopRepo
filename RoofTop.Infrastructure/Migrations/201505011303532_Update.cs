namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealEstateAds", "HtmlContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealEstateAds", "HtmlContent");
        }
    }
}
