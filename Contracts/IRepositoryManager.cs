namespace Contracts;

public interface IRepositoryManager
{
    IArticleRepository Article { get;  }
    Task SaveAsync();
}