using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Repository.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasData(
            new Article
            {
                IdArticle = 1,
                Title = "Introduction to C#",
                Tag = "Programming",
                Year = 2021,
                Author = "John Doe"
            },
            new Article
            {
                IdArticle = 2,
                Title = "Advanced C# Techniques",
                Tag = "Programming",
                Year = 2022,
                Author = "Jane Smith"
            },
            new Article
            {
                IdArticle = 3,
                Title = "Entity Framework Core",
                Tag = "Database",
                Year = 2021,
                Author = "Alice Johnson"
            },
            new Article
            {
                IdArticle = 4,
                Title = "ASP.NET Core Basics",
                Tag = "Web Development",
                Year = 2020,
                Author = "Bob Brown"
            },
            new Article
            {
                IdArticle = 5,
                Title = "LINQ Fundamentals",
                Tag = "Programming",
                Year = 2019,
                Author = "Charlie Davis"
            },
            new Article
            {
                IdArticle = 6,
                Title = "Blazor for Beginners",
                Tag = "Web Development",
                Year = 2021,
                Author = "Diana Evans"
            }
        );
    }
}