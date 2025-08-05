using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Tournament;

namespace PokemonGameAPI.Application.Validators.Tournament
{
    public class TournamentUpdateValidator : AbstractValidator<TournamentUpdateDto>
    {
        public TournamentUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("StartDate cannot be in the past.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("EndDate must be after StartDate.");


        }
    }
}
