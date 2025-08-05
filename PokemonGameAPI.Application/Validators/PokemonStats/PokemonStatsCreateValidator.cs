using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Application.Validators.PokemonStats
{
    public class PokemonStatsCreateDtoValidator : AbstractValidator<PokemonStatsRequestDto>
    {
        public PokemonStatsCreateDtoValidator()
        {
            RuleFor(x => x.Level)
                .InclusiveBetween(1, 100).WithMessage("Level must be between 1 and 100.");

            RuleFor(x => x.ExperiencePoints)
                .GreaterThanOrEqualTo(0).WithMessage("ExperiencePoints must be 0 or greater.");

            RuleFor(x => x.HealthPoints)
                .GreaterThan(0).WithMessage("HealthPoints must be greater than 0.");

            RuleFor(x => x.MaxHealthPoints)
                .GreaterThan(0).WithMessage("MaxHealthPoints must be greater than 0.")
                .GreaterThanOrEqualTo(x => x.HealthPoints)
                .WithMessage("MaxHealthPoints must be greater than or equal to HealthPoints.");

            RuleFor(x => x.AttackPoints)
                .GreaterThanOrEqualTo(0).WithMessage("AttackPoints must be 0 or greater.");

            RuleFor(x => x.DefensePoints)
                .GreaterThanOrEqualTo(0).WithMessage("DefensePoints must be 0 or greater.");

        }
    }
}
