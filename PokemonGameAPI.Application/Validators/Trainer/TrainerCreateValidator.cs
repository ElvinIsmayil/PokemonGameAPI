using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Trainer;

namespace PokemonGameAPI.Application.Validators.Trainer
{
    public class TrainerCreateDtoValidator : AbstractValidator<TrainerRequestDto>
    {
        public TrainerCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 100).WithMessage("Level must be between 1 and 100.");

            RuleFor(x => x.ExperiencePoints)
                .GreaterThanOrEqualTo(0).WithMessage("ExperiencePoints must be 0 or greater.");



            RuleForEach(x => x.TrainerPokemonIds)
                .GreaterThan(0).WithMessage("Each Pokémon ID must be greater than 0.");

            RuleForEach(x => x.BadgeIds)
                .GreaterThan(0).WithMessage("Each Badge ID must be greater than 0.");

            RuleForEach(x => x.TournamentIds)
                .GreaterThan(0).WithMessage("Each Tournament ID must be greater than 0.");
        }
    }
}
