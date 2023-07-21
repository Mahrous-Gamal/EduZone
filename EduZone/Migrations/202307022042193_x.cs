namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.P_Registration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.P_Registration");
        }
    }
}
