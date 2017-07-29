namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateByVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppLunch.Venue", "UpdateBy", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("AppLunch.Venue", "City", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("AppLunch.Venue", "UpadateBy");
        }
        
        public override void Down()
        {
            AddColumn("AppLunch.Venue", "UpadateBy", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("AppLunch.Venue", "City", c => c.String(maxLength: 50, unicode: false));
            DropColumn("AppLunch.Venue", "UpdateBy");
        }
    }
}
