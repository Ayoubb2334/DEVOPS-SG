using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using FluentValidation;

namespace API.Middleware;

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
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            NotFoundException => (HttpStatusCode.NotFound, exception.Message),
            ValidationException validationEx => (HttpStatusCode.BadRequest,
                string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage))),
            _ => (HttpStatusCode.InternalServerError, "Une erreur interne est survenue.")
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            _logger.LogError(exception, "Erreur non gérée");

        context.Response.StatusCode = (int)statusCode;

        var payload = JsonSerializer.Serialize(new { error = message });
        return context.Response.WriteAsync(payload);
    }
}