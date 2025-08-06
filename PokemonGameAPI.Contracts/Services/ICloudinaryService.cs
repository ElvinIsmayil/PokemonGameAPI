using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
