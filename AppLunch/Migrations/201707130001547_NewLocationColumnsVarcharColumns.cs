namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewLocationColumnsVarcharColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppLunch.Location", "Address", c => c.String(maxLength: 50, unicode: false));
            AddColumn("AppLunch.Location", "City", c => c.String(maxLength: 50, unicode: false));
            AddColumn("AppLunch.Location", "State", c => c.String(maxLength: 2, unicode: false));
            AddColumn("AppLunch.Location", "Zip", c => c.String(maxLength: 5, unicode: false));
            AlterColumn("AppLunch.Location", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("AppLunch.Location", "CreateBy", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("AppLunch.Member", "FirstName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("AppLunch.Member", "LastName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("AppLunch.Member", "IdentityID", c => c.String(nullable: false, maxLength: 128, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("AppLunch.Member", "IdentityID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("AppLunch.Member", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("AppLunch.Member", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("AppLunch.Location", "CreateBy", c => c.String(nullable: false));
            AlterColumn("AppLunch.Location", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("AppLunch.Location", "Zip");
            DropColumn("AppLunch.Location", "State");
            DropColumn("AppLunch.Location", "City");
            DropColumn("AppLunch.Location", "Address");
        }
    }
}
