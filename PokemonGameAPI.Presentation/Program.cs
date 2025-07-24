using PokemonGameAPI.Application.Extensions;
using PokemonGameAPI.Infrastructure.Extensions;
using PokemonGameAPI.Persistence.Extensions;
using PokemonGameAPI.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterDataAccessServices(config);
builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterAPIServices(config);
builder.Services.RegisterInfrastructureServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
