namespace AppLunch.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class VenueTableWithLinker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppLunch.Venue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 50, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationVenue",
                c => new
                    {
                        LocationRefId = c.Int(nullable: false),
                        VenueRefId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "Schema", "AppLunch" },
                })
                .PrimaryKey(t => new { t.LocationRefId, t.VenueRefId })
                .ForeignKey("AppLunch.Location", t => t.LocationRefId, cascadeDelete: true)
                .ForeignKey("AppLunch.Venue", t => t.VenueRefId, cascadeDelete: true)
                .Index(t => t.LocationRefId)
                .Index(t => t.VenueRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationVenue", "VenueRefId", "AppLunch.Venue");
            DropForeignKey("dbo.LocationVenue", "LocationRefId", "AppLunch.Location");
            DropIndex("dbo.LocationVenue", new[] { "VenueRefId" });
            DropIndex("dbo.LocationVenue", new[] { "LocationRefId" });
            DropTable("dbo.LocationVenue",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "Schema", "AppLunch" },
                });
            DropTable("AppLunch.Venue");
        }
    }
}
