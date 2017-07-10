namespace AppLunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInviteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppLunch.Invite",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        InviteeEmail = c.String(maxLength: 256),
                        Inviter = c.String(maxLength: 256),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("AppLunch.Invite");
        }
    }
}
