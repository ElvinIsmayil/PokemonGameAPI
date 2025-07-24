using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Application.Validators.TrainerPokemon
{
    public class TrainerPokemonCreateDtoValidator : AbstractValidator<TrainerPokemonCreateDto>
    {
        public TrainerPokemonCreateDtoValidator()
        {
            RuleFor(x => x.TrainerId)
                .GreaterThan(0).WithMessage("TrainerId must be greater than 0.");

            RuleFor(x => x.PokemonId)
                .GreaterThan(0).WithMessage("PokemonId must be greater than 0.");

            RuleFor(x => x.TrainerPokemonStatsId)
                .GreaterThan(0).WithMessage("TrainerPokemonStatsId must be greater than 0.");
        }
    }
}
