namespace AppLunch.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLocationVenueSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.LocationVenue", newSchema: "AppLunch");
            AlterTableAnnotations(
                "AppLunch.LocationVenue",
                c => new
                    {
                        LocationRefId = c.Int(nullable: false),
                        VenueRefId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Schema",
                        new AnnotationValues(oldValue: "AppLunch", newValue: null)
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
                "AppLunch.LocationVenue",
                c => new
                    {
                        LocationRefId = c.Int(nullable: false),
                        VenueRefId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Schema",
                        new AnnotationValues(oldValue: null, newValue: "AppLunch")
                    },
                });
            
            MoveTable(name: "AppLunch.LocationVenue", newSchema: "dbo");
        }
    }
}
