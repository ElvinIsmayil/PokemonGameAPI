using PokemonGameAPI.Contracts.DTOs.Log;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class LogDataService : ILogDataService
    {
        private readonly IGenericRepository<LogData> _repository;

        public LogDataService(IGenericRepository<LogData> repository)
        {
            _repository = repository;
        }

        public async Task LogAsync(LogDataDto model)
        {
            var logData = new LogData
            {
                Source = model.Source,
                Level = model.Level,
                CreatedAt = DateTime.UtcNow,
                Message = model.Message,
                RequestId = model.RequestId,
            };

            await _repository.CreateAsync(logData);

        }
    }
}
