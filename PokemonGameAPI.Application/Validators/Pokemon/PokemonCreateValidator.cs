using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.Pokemon;

namespace PokemonGameAPI.Application.Validators.Pokemon
{
    public class PokemonCreateDtoValidator : AbstractValidator<PokemonCreateDto>
    {
        public PokemonCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.PokemonCategoryId)
                .GreaterThan(0).WithMessage("PokemonCategoryId must be greater than 0.");



            RuleFor(x => x.AbilitiesIds)
                .NotEmpty().WithMessage("At least one ability must be selected.");

            RuleForEach(x => x.AbilitiesIds)
                .GreaterThan(0).WithMessage("Ability IDs must be greater than 0.");

        }
    }
}
