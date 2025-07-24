using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;

namespace PokemonGameAPI.Application.Validators.PokemonCategoryValidator
{
    public class PokemonCategoryCreateValidator : AbstractValidator<PokemonCategoryCreateDto>
    {
        public PokemonCategoryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
