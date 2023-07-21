namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        userId = c.String(),
                        SenderId = c.String(),
                        GroupCode = c.String(),
                        TimeOfNotify = c.DateTime(nullable: false),
                        TypeOfPost = c.String(),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInNotificationPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConnectionID = c.String(),
                        TimeOfLastOpen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInNotificationPages");
            DropTable("dbo.Notifications");
        }
    }
}
