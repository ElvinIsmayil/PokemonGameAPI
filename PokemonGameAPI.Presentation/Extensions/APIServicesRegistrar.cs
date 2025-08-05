using Microsoft.OpenApi.Models;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Presentation.ExceptionHandlers;
using Serilog;

namespace PokemonGameAPI.Presentation.Extensions
{
    public static class APIServicesRegistrar
    {
        public static IServiceCollection RegisterAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Cors Configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Swagger Jwt Token Authorization Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PokemonGame.API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });

            // Exception Handlers
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<UnauthorizedExceptionHandler>();
            services.AddProblemDetails();

            //Logger registration
            services.AddSingleton(Log.Logger);

            // Configuration Settings
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<PokemonSettings>(configuration.GetSection("PokemonSettings"));


            return services;
        }
    }
}
