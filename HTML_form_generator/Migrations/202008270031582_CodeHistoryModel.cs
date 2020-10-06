namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeHistoryModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeHistories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Code = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CodeHistories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CodeHistories", new[] { "UserId" });
            DropTable("dbo.CodeHistories");
        }
    }
}
