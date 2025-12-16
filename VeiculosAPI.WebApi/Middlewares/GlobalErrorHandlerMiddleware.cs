using FluentValidation;
using System.Text.Json;

namespace VeiculosAPI.WebApi.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case ValidationException e:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    var errors = e.Errors.Select(f => f.ErrorMessage).ToList();
                    await response.WriteAsync(JsonSerializer.Serialize(new { errors }));
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    await response.WriteAsync(JsonSerializer.Serialize(new { message = "Ocorreu um erro interno no servidor." }));
                    break;
            }
        }
    }
}