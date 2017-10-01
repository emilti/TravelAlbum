namespace TravelAlbum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class files : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Images", newName: "TravelImages");
            AlterColumn("dbo.TravelImages", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TravelImages", "Content", c => c.Int(nullable: false));
            RenameTable(name: "dbo.TravelImages", newName: "Images");
        }
    }
}
