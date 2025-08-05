using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PokemonGameAPI.Persistence.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PokemonGameDbContext>
    {
        public PokemonGameDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../PokemonGameAPI.Presentation");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<PokemonGameDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PokemonGameDbContext(optionsBuilder.Options);
        }
    }
}
