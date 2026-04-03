using System.Net;
using System.Text.Json;
using BankRecon.Application.Common.Exceptions;

namespace BankRecon.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

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
            _logger.LogError(ex, "An unhandled exception occurred: {ExceptionMessage}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            EntityNotFoundException ex => (
                HttpStatusCode.NotFound,
                ex.Message,
                (IDictionary<string, string[]>?)null
            ),
            ValidationException ex => (
                HttpStatusCode.BadRequest,
                "Validation failed",
                (IDictionary<string, string[]>?)ex.Errors
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "An internal server error occurred",
                (IDictionary<string, string[]>?)null
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode,
            message,
            errors,
            success = false
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
