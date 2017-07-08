namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUserToMember : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "AppLunch.User", newName: "Member");
        }
        
        public override void Down()
        {
            RenameTable(name: "AppLunch.Member", newName: "User");
        }
    }
}
