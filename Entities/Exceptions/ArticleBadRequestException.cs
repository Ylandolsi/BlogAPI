namespace Models.Exceptions;

public class ArticleBadRequestException : BadRequestException
{
    public ArticleBadRequestException() : base("Article object is null")
    {
    }
    
}