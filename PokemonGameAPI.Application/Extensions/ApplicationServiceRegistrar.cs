using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonGameAPI.Application.Profiles;
using PokemonGameAPI.Application.Services;
using PokemonGameAPI.Contracts.Services;

namespace PokemonGameAPI.Application.Extensions
{
    public static class ApplicationServiceRegistrar
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IPokemonAbilityService, PokemonAbilityService>();
            services.AddScoped<IPokemonCategoryService, PokemonCategoryService>();
            services.AddScoped<IPokemonStatsService, PokemonStatsService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ITrainerPokemonService, TrainerPokemonService>();
            services.AddScoped<IUserService, UserService>();




            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
