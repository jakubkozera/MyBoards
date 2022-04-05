using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class ViewTopAuthorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW View_TopAuthors AS
            SELECT TOP 5 u.FullName, COUNT(*) as [WorkItemsCreated]
            FROM Users u
            JOIN WorkItems wi on wi.AuthorId = u.Id
            GROUP BY u.Id, u.FullName
            ORDER BY [WorkItemsCreated] DESC
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP VIEW View_TopAuthors 
            ");
        }
    }
}
