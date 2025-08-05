using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Gym;

namespace PokemonGameAPI.Application.Validators.GymValidator
{
    public class GymCreateValidator : AbstractValidator<GymRequestDto>
    {
        public GymCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gym name is required.")
                .MaximumLength(100).WithMessage("Gym name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");


            RuleFor(x => x.GymLeaderTrainerId)
                .GreaterThan(0).WithMessage("GymLeaderTrainerId must be greater than zero.");

        }
    }
}
