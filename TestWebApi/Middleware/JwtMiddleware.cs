using System.Security.Claims;
using TestWebApi.Abstractions;
using TestWebApi.Helpers;

namespace TestWebApi.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IUserRepository userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var claimsPrincipal = new Jwt(_configuration).ValidateToken(token);
        var claim = claimsPrincipal.FindFirst(ClaimTypes.Email);
        var userEmail = claim.Value;
        if (userEmail != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.FindByEmail(userEmail);
        }

        await _next(context);
    }
}