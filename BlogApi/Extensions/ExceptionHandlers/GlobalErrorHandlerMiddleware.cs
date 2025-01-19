using System.Net;
using System.Text.Json;
using Models;
using Models.Exceptions;

namespace BlogApi.Extensions.ExceptionHandlers;

// private readonly RequestDelegate _next; 
// private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
//
// public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
// {
//     _next = next;
//     _logger = logger;
// }
public class GlobalErrorHandlerMiddleware ( RequestDelegate _next , ILogger<GlobalErrorHandlerMiddleware> _logger) {
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); 
            // next is a delegate that accepts httpcontext as a paramter and returns a task
            // invoke the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        string message;


        if (ex is NotFoundException)
        {
            status = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (ex is BadRequestException)
        {
            status = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else // for any other exception threwed 
        {
            status = HttpStatusCode.InternalServerError;
            message = "An unexpected error occurred.";
        }

        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(
            JsonSerializer.Serialize(
                new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message
                }
            ));
    }
}

// public static class GlobalErrorHandlerMiddlewareExtensions
// {
//     public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder builder)
//     {
//         return builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
//     }
// }
