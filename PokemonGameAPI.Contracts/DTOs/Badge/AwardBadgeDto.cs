using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGameAPI.Contracts.DTOs.Badge
{
    public record AwardBadgeDto
    {
        public int TrainerId { get; set; } = default!;
        public int GymId { get; set; } = default!;
    }
}
