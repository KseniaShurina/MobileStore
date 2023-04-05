using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Services;
using MobileStore.Infrastructure.Configurations;

namespace MobileStore.Core.Configurations;

public static class CoreDependenciesConfiguration
{
    public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructureDependencies(configuration);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}