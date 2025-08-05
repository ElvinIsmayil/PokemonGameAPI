using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class BadgeService : GenericService<Badge, BadgeRequestDto, BadgeResponseDto>, IBadgeService
    {

        private readonly IImageService _imageService;
        private const string folderName = "badges";

        public BadgeService(IGenericRepository<Badge> repository, IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService) : base(repository, unitOfWork, mapper)
        {
            _imageService = imageService;
        }

        public override Task<BadgeResponseDto> GetByIdAsync(int id, params Func<IQueryable<Badge>, IQueryable<Badge>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                includes = new Func<IQueryable<Badge>, IQueryable<Badge>>[]
                {
                     q => q.Include(b => b.Gym)
                };
            }
            ;

            return base.GetByIdAsync(id, includes);
        }

        public override async Task<PagedResponse<BadgeResponseDto>> GetAllAsync(int pageNumber, int pageSize, params Func<IQueryable<Badge>, IQueryable<Badge>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                includes = new Func<IQueryable<Badge>, IQueryable<Badge>>[]
                {
                     q => q.Include(b => b.Gym),
                };
            }
            return await base.GetAllAsync(pageNumber, pageSize, includes);
        }

        public async Task<BadgeResponseDto> UploadImgAsync(int id, IFormFile imageFile)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
            }
            var entity = await _repository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            var fileValidationErrors = _imageService.ValidateFileType(imageFile);
            if (fileValidationErrors.Count > 0)
            {
                throw new ValidationException(string.Join(",", fileValidationErrors));
            }
            (string? imageUrl, List<string> validationErrors) = await _imageService.SaveImageAsync(imageFile, folderName, entity.ImageUrl);
            if (validationErrors.Count > 0)
            {
                throw new InvalidOperationException(string.Join(", ", validationErrors));
            }
            entity.ImageUrl = imageUrl;
            var updatedEntity = await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BadgeResponseDto>(updatedEntity);
        }

    }
}
