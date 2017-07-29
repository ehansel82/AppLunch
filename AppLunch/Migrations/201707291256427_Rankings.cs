namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rankings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppLunch.Ranking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Stars = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Member_ID = c.Int(nullable: false),
                        Venue_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("AppLunch.Member", t => t.Member_ID, cascadeDelete: true)
                .ForeignKey("AppLunch.Venue", t => t.Venue_ID, cascadeDelete: true)
                .Index(t => t.Member_ID)
                .Index(t => t.Venue_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AppLunch.Ranking", "Venue_ID", "AppLunch.Venue");
            DropForeignKey("AppLunch.Ranking", "Member_ID", "AppLunch.Member");
            DropIndex("AppLunch.Ranking", new[] { "Venue_ID" });
            DropIndex("AppLunch.Ranking", new[] { "Member_ID" });
            DropTable("AppLunch.Ranking");
        }
    }
}
