using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    IdArticle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.IdArticle);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "IdArticle", "Author", "Tag", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "John Doe", "Programming", "Introduction to C#", 2021 },
                    { 2, "Jane Smith", "Programming", "Advanced C# Techniques", 2022 },
                    { 3, "Alice Johnson", "Database", "Entity Framework Core", 2021 },
                    { 4, "Bob Brown", "Web Development", "ASP.NET Core Basics", 2020 },
                    { 5, "Charlie Davis", "Programming", "LINQ Fundamentals", 2019 },
                    { 6, "Diana Evans", "Web Development", "Blazor for Beginners", 2021 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
