using CW21Ta23.Service.Exceptions;
using CW21Ta23.WebApi.ResultPatterns;

namespace CW21Ta23.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode = 500;
        string message = "Internal Server Error";

        if (exception is BaseAppException appException)
        {
            statusCode = appException.StatusCode;
            message = appException.Message;
        }

        context.Response.StatusCode = statusCode;

        var response = GenericResult<object>.Failure(message, statusCode);

        await context.Response.WriteAsJsonAsync(response);
    }
}