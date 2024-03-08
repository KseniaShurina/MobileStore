using Microsoft.Extensions.DependencyInjection;
using MobileStore.Common.Abstractions.Services;
using MobileStore.Common.Services;

namespace MobileStore.Core.Identity.Setup;

public static class RegisterIdentityServicesExtensions
{
    public static IServiceCollection RegisterIdentityDependencies(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IWriteIdentityService>(sp => sp.GetRequiredService<IIdentityService>());
        services.AddScoped<IReadIdentityService>(sp => sp.GetRequiredService<IIdentityService>());

        return services;
    }
}