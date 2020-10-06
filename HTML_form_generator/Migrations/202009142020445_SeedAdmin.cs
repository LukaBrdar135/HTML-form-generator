namespace HTML_form_generator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'42cb6603-432b-429e-a9f5-5fafefa339ae', N'admin', N'admin', N'admin@app.com', 0, N'AIZhz17rgx9VcOhfTSI4bhkQUIWAb02arYjpSj8BttZkhrveQuSGllFY9hjVJqr6Zg==', N'deb75a77-64bf-4965-994f-4284d67a42c8', NULL, 0, 0, NULL, 1, 0, N'admin@app.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6e1c468e-fcb6-49f7-93e1-095982e5f331', N'Admin')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42cb6603-432b-429e-a9f5-5fafefa339ae', N'6e1c468e-fcb6-49f7-93e1-095982e5f331')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
