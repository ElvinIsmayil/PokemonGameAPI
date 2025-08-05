using PokemonGameAPI.Application.Extensions;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Infrastructure.Extensions;
using PokemonGameAPI.Persistence.Extensions;
using PokemonGameAPI.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.RegisterDataAccessServices(config);
builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterAPIServices(config);
builder.Services.RegisterInfrastructureServices(config);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

try
{
    Log.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start correctly.");
}
finally
{
    Log.CloseAndFlush();
}
