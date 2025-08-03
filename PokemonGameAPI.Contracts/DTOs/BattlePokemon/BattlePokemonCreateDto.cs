using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGameAPI.Contracts.DTOs.BattlePokemon
{
    public record BattlePokemonCreateDto
    {
        public int BattleId { get; set; }
        public int TrainerPokemonId { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentLevel { get; set; }
        public bool IsFainted => CurrentHP <= 0;
    }
}
