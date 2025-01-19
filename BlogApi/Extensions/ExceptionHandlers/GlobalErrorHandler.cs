using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Extensions.ExceptionHandlers;

public class GlobalErrorHandler : IExceptionHandler
{
    private readonly ILogger<GlobalErrorHandler> _logger;

    public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
    {
        _logger = logger;
    }
    // Cancellation token is used to notify that the operation should be cancelled
    // exp : when the user cancels the request ( it should be cancelled )
    // for exp to upload large file and Cancelation  happens before the file is uploaded 100 % 
    // the process should end and the file should not be uploaded
    
    
    // since the method is async , the process of handeling can be cancelled also if the user cancels the request
    
    // value task can be used to return a value or a task ( not necessarrly have to run an async operation )

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError($"Something went wrong: {exception.Message}");
        var problemDetails = new ProblemDetails() // problem details is a class injected 
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error"
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
