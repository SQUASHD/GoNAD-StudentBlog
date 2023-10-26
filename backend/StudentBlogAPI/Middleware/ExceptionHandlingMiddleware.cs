using System.Text.Json;
using StudentBlogAPI.Exceptions;

namespace StudentBlogAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            UserForbiddenException => StatusCodes.Status403Forbidden,
            ItemNotFoundException => StatusCodes.Status404NotFound,
            PasswordMismatchException => StatusCodes.Status400BadRequest,
            UserAlreadyExistsException => StatusCodes.Status400BadRequest,
            BadLoginRequestException => StatusCodes.Status400BadRequest,
            InvalidJwtException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError // default to 500 error
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "An unexpected server error occurred.");
        else
            _logger.LogInformation(exception, "A handled exception occurred.");

        var errorDetails = new ErrorDetails
        {
            StatusCode = statusCode,
            Message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var jsonResponse = JsonSerializer.Serialize(errorDetails);

        return context.Response.WriteAsync(jsonResponse);
    }
}