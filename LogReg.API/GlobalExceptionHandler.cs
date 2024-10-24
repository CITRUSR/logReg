using FluentValidation;

namespace LogReg.API;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case ValidationException valEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(valEx.Errors);
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
        }
    }
}
