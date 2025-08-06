﻿using FluentValidation;

namespace PokemonGameAPI.Application.Validators
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(x => x)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");
        }
    }
}
