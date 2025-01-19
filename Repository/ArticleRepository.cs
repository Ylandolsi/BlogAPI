using Contracts;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Repository;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(DbContextRepository repositoryContext) : base(repositoryContext)
    {
    }

    public async Task CreateArticle(Article article)
    {
        await CreateAsync(article);
    }

    public async Task DeleteArticle(Article article)
    {
        await Delete(article);
    }

    public async Task UpdateArticle(Article article)
    {
        await Update(article);
    }

    public async Task<IEnumerable<Article>> GetAllArticlesAsync(bool trackChanges)
        => await FindAllAsync(trackChanges);

    public async Task<IEnumerable<Article>> GetArticlesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        =>
            await FindByConditionAsync(x => ids.Contains(x.IdArticle), trackChanges);

    public async Task<Article> GetArticleAsync(int articleId, bool trackChanges)
    {
        var listArticle = await FindByConditionAsync(x => x.IdArticle.Equals(articleId), trackChanges);
        return listArticle.FirstOrDefault();
    }
}
