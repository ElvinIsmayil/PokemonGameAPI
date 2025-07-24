using FluentValidation;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;

namespace PokemonGameAPI.Application.Validators.PokemonAbilityValidator
{
    public class PokemonAbilityCreateDtoValidator : AbstractValidator<PokemonAbilityCreateDto>
    {
        public PokemonAbilityCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ability name is required.")
                .MaximumLength(100).WithMessage("Ability name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        }
    }
}
