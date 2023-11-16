using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Common.Configurations;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Services;
using MobileStore.Infrastructure.Configurations;

namespace MobileStore.Core.Configurations;

public static class CoreDependenciesConfiguration
{
    /// <summary>
    /// It manages of life cycle of services
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <returns></returns>
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityDependencies();

        services.RegisterInfrastructureDependencies(configuration);

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IContentService, ContentService>();

        return services;
    }
}