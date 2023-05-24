using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Infrastructure.Contexts;

namespace MobileStore.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DefaultContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("Database:ConnectionString");
                var isLogEnabled = configuration.GetValue<bool>("Database:IsLogEnabled");
                options.UseNpgsql(connectionString);
                if (isLogEnabled)
                {
                    options.LogTo(System.Console.WriteLine);
                }
            });
        }
    }
}
