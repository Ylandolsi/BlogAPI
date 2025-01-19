using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Configurations;

namespace Repository;

public class DbContextRepository : DbContext
{
    public DbContextRepository(DbContextOptions<DbContextRepository>options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration()); 
    }
    DbSet<Article>?  Articles { get; set; }
    
    
}