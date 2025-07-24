using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Battle;

namespace PokemonGameAPI.Application.Validators.Battle
{
    public class BattleCreateDtoValidator : AbstractValidator<BattleCreateDto>
    {
        public BattleCreateDtoValidator()
        {
            RuleFor(x => x.Trainer1Id)
                .GreaterThan(0).WithMessage("Trainer1Id must be greater than 0.");

            RuleFor(x => x.Trainer2Id)
                .GreaterThan(0).WithMessage("Trainer2Id must be greater than 0.")
                .NotEqual(x => x.Trainer1Id).WithMessage("Trainer1Id and Trainer2Id cannot be the same.");

            RuleFor(x => x.StartTime)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Start time cannot be in the future.")
                .When(x => x.StartTime.HasValue);

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.")
                .When(x => x.StartTime.HasValue && x.EndTime.HasValue);

            RuleFor(x => x.Trainer1Pokemons)
                .NotEmpty().WithMessage("Trainer1 must have at least one Pokémon.");

            RuleFor(x => x.Trainer2Pokemons)
                .NotEmpty().WithMessage("Trainer2 must have at least one Pokémon.");

            RuleForEach(x => x.Trainer1Pokemons)
                .GreaterThan(0).WithMessage("Pokémon IDs in Trainer1Pokemons must be greater than 0.");

            RuleForEach(x => x.Trainer2Pokemons)
                .GreaterThan(0).WithMessage("Pokémon IDs in Trainer2Pokemons must be greater than 0.");
        }
    }
}
