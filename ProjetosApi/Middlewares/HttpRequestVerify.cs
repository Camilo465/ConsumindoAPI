namespace ProjetosApi.Middlewares;
public class HttpRequestVerify
{
    private readonly RequestDelegate _next;

    public HttpRequestVerify(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("guidApiDataRequest"))
        {
            context.Request.Headers.Add("guidApiDataRequest", "c56a4180-65aa-42ec-a945-5fd21dec0538");
        }

        await _next(context);
    }
}
