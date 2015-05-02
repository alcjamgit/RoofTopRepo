namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RealEstateAds", "Title", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.RealEstateAds", "CreatedBy", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RealEstateAds", "CreatedBy", c => c.String());
            AlterColumn("dbo.RealEstateAds", "Title", c => c.String(maxLength: 128));
        }
    }
}
