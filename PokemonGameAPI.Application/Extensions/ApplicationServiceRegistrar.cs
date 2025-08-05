using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonGameAPI.Application.Profiles;
using PokemonGameAPI.Application.Services;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Contracts.Settings;

namespace PokemonGameAPI.Application.Extensions
{
    public static class ApplicationServiceRegistrar
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register AutoMapper with the MapperProfile
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

            // Register services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IPokemonAbilityService, PokemonAbilityService>();
            services.AddScoped<IPokemonCategoryService, PokemonCategoryService>();
            services.AddScoped<IPokemonStatsService, PokemonStatsService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ITrainerPokemonService, TrainerPokemonService>();
            services.AddScoped<ITrainerPokemonStatsService, TrainerPokemonStatsService>();
            services.AddScoped<IUserService, UserService>();

            // Register FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
