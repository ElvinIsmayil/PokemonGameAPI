using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Infrastructure.Services;

namespace PokemonGameAPI.Infrastructure.Extensions
{
    public static class InfrastructureServicesRegistrar
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}
