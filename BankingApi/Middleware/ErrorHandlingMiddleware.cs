using System.Net;
using System.Text.Json;

namespace BankingApi.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleError(context, ex);
        }
    }

    private Task HandleError(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var status = ex switch
        {
            ArgumentException => HttpStatusCode.BadRequest,      // 400
            InvalidOperationException => HttpStatusCode.Conflict, // 409
            KeyNotFoundException => HttpStatusCode.NotFound,      // 404
            _ => HttpStatusCode.InternalServerError               // 500
        };

        response.StatusCode = (int)status;

        var error = new
        {
            message = ex.Message,
            status = status.ToString(),
            code = response.StatusCode
        };

        return response.WriteAsync(JsonSerializer.Serialize(error));
    }
}
