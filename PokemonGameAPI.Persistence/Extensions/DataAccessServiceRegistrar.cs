using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonGameAPI.Domain.Repository;
using PokemonGameAPI.Persistence.Data;
using PokemonGameAPI.Persistence.Repository;

namespace PokemonGameAPI.Persistence.Extensions
{
    public static class DataAccessServiceRegistrar
    {
        public static IServiceCollection RegisterDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with SQL Server and migrations assembly
            services.AddDbContext<PokemonGameDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                        sqlOptions => sqlOptions.MigrationsAssembly("PokemonGameAPI.Persistence"))
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            // Register repositories and unit of work
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
