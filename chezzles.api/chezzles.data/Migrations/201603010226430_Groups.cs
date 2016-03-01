namespace chezzles.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Difficulty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "Group_Id", c => c.Int());
            CreateIndex("dbo.Games", "Group_Id");
            AddForeignKey("dbo.Games", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Games", new[] { "Group_Id" });
            DropColumn("dbo.Games", "Group_Id");
            DropTable("dbo.Groups");
        }
    }
}
