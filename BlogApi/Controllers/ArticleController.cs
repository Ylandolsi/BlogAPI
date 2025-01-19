using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace BlogApi.Controllers;

[Route("api/article")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ILogger<ArticleController> _logger;
    private readonly IArticleSevice _articleSevice;

    public ArticleController(IArticleSevice articleSevice, ILogger<ArticleController> logger)
    {
        _articleSevice = articleSevice;
        _logger = logger;
    }

    [HttpGet("collection")]
    public async Task<IActionResult> GetArticles()
    {
        _logger.LogInformation("Fetching all articles");
        var articles = await _articleSevice.GetAllArticlesAsync(false);
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticle(int id)
    {
        var article = await _articleSevice.GetArticleAsync(id, false);
        return Ok(article);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        await _articleSevice.DeleteArticle(id);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] Article article)
    {
        if (article == null)
        {
            return BadRequest("Article object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _articleSevice.CreateArticle(article);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateArticle( [FromBody] Article article)
    {
        if (article == null)
        {
            return BadRequest("Article object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _articleSevice.UpdateArticle( article);
        return Ok();
    }
}
