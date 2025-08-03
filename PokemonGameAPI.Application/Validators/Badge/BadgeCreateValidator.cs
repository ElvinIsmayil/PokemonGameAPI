using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Badge;

namespace PokemonGameAPI.Application.Validators.BadgeValidator
{
    public class BadgeCreateValidator : AbstractValidator<BadgeCreateDto>
    {
        public BadgeCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");



        }
    }
}
