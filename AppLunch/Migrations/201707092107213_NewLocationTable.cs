namespace AppLunch.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NewLocationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppLunch.Location",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    CreateBy = c.String(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("AppLunch.Location");
        }
    }
}