namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieGenreNameUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MovieGenres", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovieGenres", "Name", c => c.String(nullable: false));
        }
    }
}
