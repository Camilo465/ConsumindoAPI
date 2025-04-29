namespace ProjetosApi.Middlewares;
public class MiddlewareGuidApiDataRequest
{
    private readonly RequestDelegate _next;

    public MiddlewareGuidApiDataRequest(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(!context.Request.Headers.ContainsKey("guidApiDataRequest"))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("O header guidApiDataRequest não existe.");
            return;
        }

        await _next(context);
    }
}
