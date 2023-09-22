using System.Security.Claims;
using MobileStore.Common.Abstractions.Services;

namespace MobileStore.Presentation.Mvc.Middleware;

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
    /// <param name="writeIdentityService"></param>
    public async Task Invoke(HttpContext context, IWriteIdentityService writeIdentityService)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            writeIdentityService.SetCurrent(context.User);
        }

        await _next(context);
    }
}