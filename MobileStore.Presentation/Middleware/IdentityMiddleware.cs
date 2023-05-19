using System.Security.Claims;
using MobileStore.Common.Identity;

namespace MobileStore.Presentation.Middleware;

/// <summary>
/// IdentityMiddleware
/// </summary>
public class IdentityMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="next"></param>
    public IdentityMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var userIdString = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            IdentityState.SetCurrent(int.Parse(userIdString));
        }

        await _next(context);
    }
}