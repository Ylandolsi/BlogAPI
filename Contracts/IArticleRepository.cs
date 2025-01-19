using Models.Models;

namespace Contracts;

public interface IArticleRepository
{
    Task CreateArticle(Article article);
    Task DeleteArticle(Article article);
    Task UpdateArticle(Article article);
    Task<IEnumerable<Article>> GetAllArticlesAsync(bool trackChanges);
    Task<Article> GetArticleAsync(int articleId, bool trackChanges);
    Task<IEnumerable<Article>> GetArticlesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    
    
    
}
