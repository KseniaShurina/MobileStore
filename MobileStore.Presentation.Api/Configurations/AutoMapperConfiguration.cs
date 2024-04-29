using AutoMapper;

namespace MobileStore.Presentation.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// The extension method is designed to add AutoMapper profiles to the DI
        /// dependency injection container in ASP.NET Core.
        /// </summary>
        /// <param name="services">Represents the collection of services to which we add AutoMapper</param>
        /// <param name="configure">Represents a lambda expression for AutoMapper configuration</param>
        /// <returns>The method returns the services to which the IMapper was added.</returns>
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services,
            Action<IMapperConfigurationExpression> configure)
        {
            //This allows you to define mapping profiles and other AutoMapper settings.
            var config = new MapperConfiguration(configure.Invoke);
            //This helps detect mapping errors at compile time.
            config.CompileMappings();
            //We create an instance of IMapper. This IMapper will be used to perform object mappings.
            var mapper = config.CreateMapper();
            //Add the created IMapper to the collection of services as a singleton.
            //This ensures that the same IMapper instance is used for each request.
            services.AddSingleton(mapper);

            return services;
        }
    }
}
