using System.IdentityModel.Tokens.Jwt;
using Simplest.Backend.API.Application;

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

        var tokenHandle = new JwtSecurityTokenHandler();

        var tokenDecoded = tokenHandle.ReadJwtToken(token).Payload;

        if (!tokenDecoded.ContainsKey("c"))
        {
            context.Response.StatusCode = 401;

            var response = ResponseDto<string>.Fail("Invalid token");

            await context.Response.WriteAsJsonAsync<ResponseDto<string>>(response);
        }

        var company = tokenDecoded["c"];

        await this.next(context);
    }
}
