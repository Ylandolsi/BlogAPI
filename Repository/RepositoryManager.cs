using Contracts;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    // Lazy will allows us to defer the creation of the repository
    // until it is needed for the first time
    private readonly DbContextRepository _repositoryContext;
    private readonly Lazy<IArticleRepository> _articleRepository;

    public RepositoryManager(DbContextRepository repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _articleRepository = new Lazy<IArticleRepository>(() => new ArticleRepository(_repositoryContext));
    }

    public IArticleRepository Article => _articleRepository.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}

