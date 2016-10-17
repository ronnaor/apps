namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Web = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Web = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        Image = c.String(),
                        Video = c.String(),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.Fan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Seniority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "PostID", "dbo.Post");
            DropIndex("dbo.Comment", new[] { "PostID" });
            DropTable("dbo.Fan");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
