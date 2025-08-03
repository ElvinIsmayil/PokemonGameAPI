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
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<Badge> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private const string folderName = "badges";

        public BadgeService(IRepository<Badge> repository, IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<BadgeReturnDto> CreateAsync(BadgeCreateDto model)
        {
            var entity = _mapper.Map<Badge>(model);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BadgeReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
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

            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                _imageService.DeleteImage(entity.ImageUrl);
            }

            var result = await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }


        public async Task<PagedResponse<BadgeListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery();

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<BadgeListItemDto>>(entities);

            return new PagedResponse<BadgeListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<BadgeReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<BadgeReturnDto>(entity);

        }

        public async Task<BadgeReturnDto> UpdateAsync(int id, BadgeUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BadgeReturnDto>(updatedEntity);
        }

        public async Task<BadgeReturnDto> UploadImgAsync(int id, IFormFile imageFile)
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
            return _mapper.Map<BadgeReturnDto>(updatedEntity);
        }

    }
}
