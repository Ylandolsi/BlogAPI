namespace Models.Exceptions;

public class IdBadRequestException : BadRequestException
{
    public IdBadRequestException() : base("Id is null")
    {
    }
    
}