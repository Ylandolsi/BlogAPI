using Contracts;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Repository;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(DbContextRepository repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateArticle(Article article)
    {
        Create(article);
    }

    public void DeleteArticle(Article article)
    {
        Delete(article);
    }

    public void UpdateArticle(Article article)
    {
        Update(article);
    }

    public async Task<IEnumerable<Article>> GetAllArticlesAsync(bool trackChanges)
        => await FindAll(trackChanges).ToListAsync();

    public async Task<IEnumerable<Article>> GetArticlesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        =>
            await FindByCondition(x => ids.Contains(x.IdArticle), trackChanges).ToListAsync();
    
    public async Task<Article> GetArticleAsync(int articleId, bool trackChanges)
    => await FindByCondition(x => x.IdArticle.Equals(articleId), trackChanges).FirstOrDefaultAsync();
    
    
    
}
