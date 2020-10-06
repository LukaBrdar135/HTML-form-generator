namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreMadeFormFree : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreMadeForms", "Free", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreMadeForms", "Free");
        }
    }
}
