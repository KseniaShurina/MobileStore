﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Contexts;

namespace MobileStore.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDefaultContext, DefaultContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("Database:ConnectionString");
                var isLogEnabled = configuration.GetValue<bool>("Database:IsLogEnabled");
                options.UseNpgsql(connectionString);
                if (isLogEnabled)
                {
                    options.LogTo(Console.WriteLine);
                }
            }, ServiceLifetime.Transient);
        }
    }
}
