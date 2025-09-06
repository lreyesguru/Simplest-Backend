namespace Simplest.Backend.API;

public class SimplestAuthorizationMiddleware
{
    private readonly RequestDelegate next;
    private ILogger logger;

    public SimplestAuthorizationMiddleware(RequestDelegate next, ILogger<SimplestAuthorizationMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;   
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["simplest_token"].FirstOrDefault()?.Split(" ").Last();

        if (token == "token")
        {
            await this.next(context);
        }

        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token inválido");
        }
    }
}
