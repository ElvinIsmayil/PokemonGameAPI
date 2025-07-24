using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Location;

namespace PokemonGameAPI.Application.Validators.LocationValidator
{
    public class LocationUpdateValidator : AbstractValidator<LocationUpdateDto>
    {
        public LocationUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required.")
                .MaximumLength(100).WithMessage("Location name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.WildPokemonIds)
                .NotNull().WithMessage("WildPokemonIds must be provided.")
                .Must(list => list.All(id => id > 0)).WithMessage("All WildPokemonIds must be greater than zero.");

            RuleFor(x => x.GymIds)
                .NotNull().WithMessage("GymIds must be provided.")
                .Must(list => list.All(id => id > 0)).WithMessage("All GymIds must be greater than zero.");

            RuleFor(x => x.TournamentIds)
                .NotNull().WithMessage("TournamentIds must be provided.")
                .Must(list => list.All(id => id > 0)).WithMessage("All TournamentIds must be greater than zero.");
        }
    }
}
