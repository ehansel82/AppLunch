namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewVenueColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppLunch.Venue", "Address", c => c.String(maxLength: 50, unicode: false));
            AddColumn("AppLunch.Venue", "State", c => c.String(maxLength: 2, unicode: false));
            AddColumn("AppLunch.Venue", "Zip", c => c.String(maxLength: 5, unicode: false));
            AddColumn("AppLunch.Venue", "CreateBy", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("AppLunch.Venue", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("AppLunch.Venue", "CreateDate");
            DropColumn("AppLunch.Venue", "CreateBy");
            DropColumn("AppLunch.Venue", "Zip");
            DropColumn("AppLunch.Venue", "State");
            DropColumn("AppLunch.Venue", "Address");
        }
    }
}
