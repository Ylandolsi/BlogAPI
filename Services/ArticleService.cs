using Contracts;
using Microsoft.Extensions.Logging;
using Models.Exceptions;
using Models.Models;
using Repository;

namespace Services;

public class ArticleService: IArticleSevice 
{
    private readonly IArticleRepository _articleRepository;
    private readonly DbContextRepository _contextRepository; 
    private readonly ILogger<ArticleService> _logger;
    public ArticleService(IArticleRepository articleRepository , DbContextRepository contextRepository , 
        ILogger<ArticleService> logger)
    {
        _articleRepository = articleRepository; 
        _contextRepository = contextRepository;
        _logger = logger;
    }
    private void CheckNullArticle ( Article article)
    {
        if (article == null) 
            throw new ArticleBadRequestException();
        
    }

    public async Task  CreateArticle(Article article)
    {
        CheckNullArticle(article);
        _articleRepository.CreateArticle(article);
        await _contextRepository.SaveChangesAsync(); 

        
    }
    public async Task DeleteArticle(Article article)
    {
        CheckNullArticle(article);
        _articleRepository.DeleteArticle(article);
        await _contextRepository.SaveChangesAsync(); 
    }
    public async Task  UpdateArticle( Article article)
    {
        _logger.LogInformation("Updating article");
        var checkArticle = await _articleRepository.GetArticleAsync(article.IdArticle, false  ); 
        if ( checkArticle is null)
            throw new ArticleNotFoundException();
        
        _articleRepository.UpdateArticle(article);
        await _contextRepository.SaveChangesAsync(); 

    }
    public async Task<IEnumerable<Article>> GetAllArticlesAsync(bool trackChanges)
    {
        var Articles =  await _articleRepository.GetAllArticlesAsync(trackChanges);
        if ( Articles == null)
            throw new ArticleNotFoundException();
        return Articles;
    }

    public async Task<Article> GetArticleAsync(int articleId, bool trackChanges)
    {
        if ( articleId == 0)
            throw new IdBadRequestException();
        var Articles =  await _articleRepository.GetArticleAsync(articleId, trackChanges);
        if ( Articles == null)
            throw new ArticleNotFoundException();
        return Articles;
    }
    public async Task<IEnumerable<Article>> GetArticlesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdBadRequestException();  
        return await _articleRepository.GetArticlesByIdsAsync(ids, trackChanges);
    }

    
}
