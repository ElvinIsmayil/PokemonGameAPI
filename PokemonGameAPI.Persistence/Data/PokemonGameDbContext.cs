using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Persistence.Configurations;

namespace PokemonGameAPI.Persistence.Data
{
    public class PokemonGameDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Badge> Badges { get; set; } = default!;
        public DbSet<Battle> Battles { get; set; } = default!;
        public DbSet<Gym> Gyms { get; set; } = default!;
        public DbSet<Location> Locations { get; set; } = default!;

        public DbSet<Pokemon> Pokemons { get; set; } = default!;
        public DbSet<PokemonAbility> PokemonAbilities { get; set; } = default!;
        public DbSet<PokemonCategory> PokemonCategories { get; set; } = default!;
        public DbSet<PokemonStats> PokemonStats { get; set; } = default!;

        public DbSet<Tournament> Tournaments { get; set; } = default!;
        public DbSet<Trainer> Trainers { get; set; } = default!;
        public DbSet<TrainerPokemon> TrainerPokemons { get; set; } = default!;
        public DbSet<TrainerPokemonStats> TrainerPokemonStats { get; set; } = default!;




        public PokemonGameDbContext(DbContextOptions<PokemonGameDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Trainer>()
    .Ignore(t => t.Battles);

            modelBuilder.Entity<PokemonStats>()
    .HasDiscriminator<string>("StatsType")
    .HasValue<PokemonStats>("BaseStats")
    .HasValue<TrainerPokemonStats>("TrainerStats");


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonConfiguration).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                // Apply filter only if the type is a root entity type (does not have a base type in the model)
                var baseType = entityType.BaseType;
                bool isRootEntityType = baseType == null || !typeof(BaseEntity).IsAssignableFrom(baseType.ClrType);

                if (typeof(BaseEntity).IsAssignableFrom(clrType) && isRootEntityType)
                {
                    var method = typeof(PokemonGameDbContext)
                        .GetMethod(nameof(SetGlobalQueryFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?
                        .MakeGenericMethod(clrType);

                    method?.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        private static void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

    }

}
