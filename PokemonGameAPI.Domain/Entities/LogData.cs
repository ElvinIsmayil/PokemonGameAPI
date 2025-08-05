using Microsoft.Extensions.Logging;
using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class LogData : BaseEntity
    {

        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Source { get; set; }
        public int? UserId { get; set; }
        public string? RequestId { get; set; }



    }
}
