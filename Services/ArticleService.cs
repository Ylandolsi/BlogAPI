using Contracts;
using Microsoft.Extensions.Logging;
using Models.Exceptions;
using Models.Models;
using Repository;

namespace Services;

public class ArticleService : IArticleSevice
{
    private readonly IArticleRepository _articleRepository;
    private readonly DbContextRepository _contextRepository;
    private readonly ILogger<ArticleService> _logger;

    public ArticleService(IArticleRepository articleRepository, DbContextRepository contextRepository,
        ILogger<ArticleService> logger)
    {
        _articleRepository = articleRepository;
        _contextRepository = contextRepository;
        _logger = logger;
    }

    private void CheckNullArticle(Article article)
    {
        _logger.LogInformation("Checking if article is null");
        if (article == null)
        {
            _logger.LogInformation("Article is null");
            throw new ArticleBadRequestException();
        }
        _logger.LogInformation("Article is not null");

    }

    public async Task CreateArticle(Article article)
    {
        _logger.LogInformation("Creating article");
        CheckNullArticle(article);
        _logger.LogInformation("Checking if ID article exists on DB ");
        var checkArticle = await _articleRepository.GetArticleAsync(article.IdArticle, false);
        if (checkArticle is null)
        {
            _logger.LogInformation("ID article does not exist on DB ");
            throw new ArticleNotFoundException();
        }
        _logger.LogInformation("ID article exists on DB ");
        await _articleRepository.CreateArticle(article);
        await _contextRepository.SaveChangesAsync();
        _logger.LogInformation("Article Created and Saved"); 
    }

    public async Task DeleteArticle(int idArticle)
    {
        _logger.LogInformation("Deleting article ( By ID )");
        _logger.LogInformation("Checking if article with Id exists ");
        
        var article = await _articleRepository.GetArticleAsync(idArticle, false);
        CheckNullArticle(article);
        await _articleRepository.DeleteArticle(article);
        await _contextRepository.SaveChangesAsync();
        _logger.LogInformation("Article Deleted");
        
        
    }

    public async Task UpdateArticle(Article article)
    {
        _logger.LogInformation("Updating article");
        
        CheckNullArticle(article);
        _logger.LogInformation("Checking if article exists");
        var checkArticle = await _articleRepository.GetArticleAsync(article.IdArticle, false);
        if (checkArticle is null)
        {
            _logger.LogInformation("Article does not exist");
            throw new ArticleNotFoundException();
        }

        await _articleRepository.UpdateArticle(article);
        await _contextRepository.SaveChangesAsync();
        _logger.LogInformation("Article Updated");
    }

    public async Task<IEnumerable<Article>> GetAllArticlesAsync(bool trackChanges)
    {
        _logger.LogInformation("Getting all articles");
        
        var Articles = await _articleRepository.GetAllArticlesAsync(trackChanges);
        if (Articles == null)
            throw new ArticleNotFoundException();
        return Articles;
        
    }

    public async Task<Article> GetArticleAsync(int articleId, bool trackChanges)
    {
        _logger.LogInformation("Getting article by ID");
        if (articleId == 0)
        {
            _logger.LogInformation("( Error  ) ID is 0");
            throw new IdBadRequestException();
        }
        
        var Articles = await _articleRepository.GetArticleAsync(articleId, trackChanges);
        if (Articles == null)
        {
            _logger.LogInformation("Article does not exist with the given id ");
            throw new ArticleNotFoundException();
        }
        return Articles;
    }

    public async Task<IEnumerable<Article>> GetArticlesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
    {
        _logger.LogInformation("Getting articles by IDs");
        if (ids is null)
        {
            _logger.LogInformation("IDs are null");
            throw new IdBadRequestException();
        }
        return await _articleRepository.GetArticlesByIdsAsync(ids, trackChanges);
    }
}
