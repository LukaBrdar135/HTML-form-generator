namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStyles : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Styles] ON
                INSERT INTO[dbo].[Styles]([id], [Name], [Code]) VALUES(1, N'None', NULL)
                SET IDENTITY_INSERT[dbo].[Styles] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
