using PokemonGameAPI.Application;
using PokemonGameAPI.Infrastructure;
using PokemonGameAPI.Persistence;
using PokemonGameAPI.Presentation;
using PokemonGameAPI.Presentation.ExceptionHandlers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.ClearProviders(); // ← restores Microsoft logs
builder.Logging.AddConsole();     // ← shows default messages again

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.RegisterDataAccessServices(config);
builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterAPIServices(config);
builder.Services.RegisterInfrastructureServices(config);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting PokemonGameAPI in {Environment} environment...", app.Environment.EnvironmentName);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.Information("Shutting down PokemonGameAPI...");
    Log.CloseAndFlush();
}

