namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "RealEstateAd_Id", "dbo.RealEstateAds");
            DropIndex("dbo.Images", new[] { "RealEstateAd_Id" });
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.RealEstateAds");
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Images", "FileName", c => c.String(maxLength: 128));
            AlterColumn("dbo.Images", "RealEstateAd_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.RealEstateAds", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.RealEstateAds", "Title", c => c.String(maxLength: 128));
            AlterColumn("dbo.RealEstateAds", "RoomCount", c => c.Int());
            AlterColumn("dbo.RealEstateAds", "BathCount", c => c.Int());
            AddPrimaryKey("dbo.Images", "Id");
            AddPrimaryKey("dbo.RealEstateAds", "Id");
            CreateIndex("dbo.Images", "RealEstateAd_Id");
            AddForeignKey("dbo.Images", "RealEstateAd_Id", "dbo.RealEstateAds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "RealEstateAd_Id", "dbo.RealEstateAds");
            DropIndex("dbo.Images", new[] { "RealEstateAd_Id" });
            DropPrimaryKey("dbo.RealEstateAds");
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.RealEstateAds", "BathCount", c => c.Int(nullable: false));
            AlterColumn("dbo.RealEstateAds", "RoomCount", c => c.Int(nullable: false));
            AlterColumn("dbo.RealEstateAds", "Title", c => c.String());
            AlterColumn("dbo.RealEstateAds", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Images", "RealEstateAd_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Images", "FileName", c => c.Guid(nullable: false));
            AlterColumn("dbo.Images", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.RealEstateAds", "Id");
            AddPrimaryKey("dbo.Images", "Id");
            CreateIndex("dbo.Images", "RealEstateAd_Id");
            AddForeignKey("dbo.Images", "RealEstateAd_Id", "dbo.RealEstateAds", "Id");
        }
    }
}
