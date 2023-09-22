using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Common.Configurations;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Services;
using MobileStore.Infrastructure.Configurations;

namespace MobileStore.Core.Configurations;

public static class CoreDependenciesConfiguration
{
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