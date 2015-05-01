namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Caption", c => c.String(maxLength: 128));
            AlterColumn("dbo.Images", "FileName", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "FileName", c => c.String());
            DropColumn("dbo.Images", "Caption");
        }
    }
}
