using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Location;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(IRepository<Location> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LocationReturnDto> CreateAsync(LocationCreateDto model)
        {
            var entity = _mapper.Map<Location>(model);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<LocationReturnDto>(entity);
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
            var result = await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<PagedResponse<LocationListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery();

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<LocationListItemDto>>(entities);

            return new PagedResponse<LocationListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<LocationReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<LocationReturnDto>(entity);
        }

        public async Task<LocationReturnDto> UpdateAsync(int id, LocationUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LocationReturnDto>(updatedEntity);
        }
    }
}
