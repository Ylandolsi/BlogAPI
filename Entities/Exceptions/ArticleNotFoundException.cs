namespace Models.Exceptions;

public class ArticleNotFoundException : NotFoundException
{
    public ArticleNotFoundException() : base("Article not found")
    {
    }
    
}