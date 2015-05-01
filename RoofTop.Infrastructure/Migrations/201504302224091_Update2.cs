namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        RealEstateAd_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealEstateAds", t => t.RealEstateAd_Id)
                .Index(t => t.RealEstateAd_Id);
            
            AddColumn("dbo.RealEstateAds", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "RealEstateAd_Id", "dbo.RealEstateAds");
            DropIndex("dbo.Images", new[] { "RealEstateAd_Id" });
            DropColumn("dbo.RealEstateAds", "Price");
            DropTable("dbo.Images");
        }
    }
}
