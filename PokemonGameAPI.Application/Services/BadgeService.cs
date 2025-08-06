using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.CustomExceptions;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;
using System.Reflection;

namespace PokemonGameAPI.Application.Services
{
    public class BadgeService : GenericService<Badge, BadgeRequestDto, BadgeResponseDto>, IBadgeService
    {
        private readonly ICloudinaryService _cloudinaryService;


        public BadgeService(IGenericRepository<Badge> repository, IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService) : base(repository, unitOfWork, mapper)
        {
            _cloudinaryService = cloudinaryService;
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

        public async Task<BadgeResponseDto> UploadImgAsync(int id, IFormFile file)
        {
            var badge = await _repository.GetEntityAsync(b => b.Id == id);
            if (badge is null)
                throw new CustomException($"Badge with ID {id} not found.");
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            if (uploadResult.Error != null)
                throw new CustomException($"Image upload failed: {uploadResult.Error.Message}");
            badge.ImageUrl = uploadResult.SecureUrl.ToString();
            await _repository.UpdateAsync(badge);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BadgeResponseDto>(badge);
        }
    }
}
