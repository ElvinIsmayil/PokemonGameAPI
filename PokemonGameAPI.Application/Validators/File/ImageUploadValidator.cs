using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace PokemonGameAPI.Application.Validators.File
{
    public class ImageUploadValidator : AbstractValidator<IFormFile>
    {
        public ImageUploadValidator()
        {
            RuleFor(file => file.Length)
            .GreaterThan(0).WithMessage("File is empty.")
            .LessThanOrEqualTo(5 * 1024 * 1024).WithMessage("Max file size is 5MB.");

            RuleFor(file => file.ContentType)
                .Must(ct => ct == "image/png" || ct == "image/jpeg")
                .WithMessage("Only PNG or JPEG are allowed.");
        }

    }
}
