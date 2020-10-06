namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StyleModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength:100),
                        Code = c.String(nullable: true),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Styles");
        }
    }
}
