namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreMadeFormModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreMadeForms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Category = c.String(nullable: false, maxLength: 100),
                        withBootstrap = c.Boolean(nullable: false),
                        Code = c.String(nullable: false),
                        StyleId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Styles", t => t.StyleId, cascadeDelete: true)
                .Index(t => t.StyleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreMadeForms", "StyleId", "dbo.Styles");
            DropIndex("dbo.PreMadeForms", new[] { "StyleId" });
            DropTable("dbo.PreMadeForms");
        }
    }
}
