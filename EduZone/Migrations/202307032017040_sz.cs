namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupMaterials", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupMaterials", "Date");
        }
    }
}
