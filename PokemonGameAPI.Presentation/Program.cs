using PokemonGameAPI.Application.Extensions;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Infrastructure.Extensions;
using PokemonGameAPI.Persistence.Extensions;
using PokemonGameAPI.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.RegisterDataAccessServices(config);
builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterAPIServices(config);
builder.Services.RegisterInfrastructureServices(config);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
