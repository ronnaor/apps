namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration33 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Map",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Map");
        }
    }
}
