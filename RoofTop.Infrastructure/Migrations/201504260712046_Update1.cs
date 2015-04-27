namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealEstateAds", "RoomCount", c => c.Int(nullable: false));
            AddColumn("dbo.RealEstateAds", "BathCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealEstateAds", "BathCount");
            DropColumn("dbo.RealEstateAds", "RoomCount");
        }
    }
}
