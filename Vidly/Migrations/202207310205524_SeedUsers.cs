namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'224186b9-9982-4c2c-9a61-b25de516c227', N'admin@vidly.com', 0, N'ALh/46ua2fpHr+BgYCiwr2Gki0dwXL6Z92wJL7K4IJ/HKXDrN420KYHpyzx5sUm29A==', N'8d7e294f-0149-425f-a1d2-ed599d0a8c51', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c7e79a86-b685-4ce0-ac28-4967c7f6f01a', N'guest@vidly.com', 0, N'ADBjvl7TEULsi6RIiPOl5mdZPGk7ScmsGqiK7s6DlKfUx1M+mC8wC48aoBICitojhA==', N'ddd823ef-acac-4be0-af1d-8e920203601f', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c53566e3-7a0a-46d1-aeb5-7ac55eb80210', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'224186b9-9982-4c2c-9a61-b25de516c227', N'c53566e3-7a0a-46d1-aeb5-7ac55eb80210')
");
        }
        
        public override void Down()
        {
            Sql(@"
DELETE FROM [dbo].[AspNetUserRoles] WHERE [RoleId] = N'c53566e3-7a0a-46d1-aeb5-7ac55eb80210'
DELETE FROM [dbo].[AspNetRoles] WHERE [Id] = N'c53566e3-7a0a-46d1-aeb5-7ac55eb80210'
DELETE FROM [dbo].[AspNetUsers] WHERE [Id] IN ('224186b9-9982-4c2c-9a61-b25de516c227', 'c7e79a86-b685-4ce0-ac28-4967c7f6f01a')
");
        }
    }
}
