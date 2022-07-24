namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingMovieGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.MovieGenres (Name) VALUES ('Comedy')");
            Sql("INSERT INTO dbo.MovieGenres (Name) VALUES ('Action')");
            Sql("INSERT INTO dbo.MovieGenres (Name) VALUES ('Family')");
            Sql("INSERT INTO dbo.MovieGenres (Name) VALUES ('Romance')");
        }
        
        public override void Down()
        {
            Sql("DELETE dbo.MovieGenres WHERE Id in (1, 2, 3, 4) ");
        }
    }
}
