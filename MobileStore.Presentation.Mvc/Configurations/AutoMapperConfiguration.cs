using AutoMapper;

namespace MobileStore.Presentation.Mvc.Configurations;

public static class AutoMapperConfiguration
{
    /// <summary>
    /// AddAutoMapperProfiles
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services,
        Action<IMapperConfigurationExpression> configure)
    {
        var config = new MapperConfiguration(configure.Invoke);
        config.CompileMappings();
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}