namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVenueColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppLunch.Venue", "UpadateBy", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("AppLunch.Venue", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("AppLunch.Venue", "UpdateDate");
            DropColumn("AppLunch.Venue", "UpadateBy");
        }
    }
}
