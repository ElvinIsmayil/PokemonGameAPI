using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class GymService : IGymService
    {
        private readonly IRepository<Gym> _repository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Trainer> _trainerRepository;
        private readonly IRepository<Badge> _badgeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GymService(
            IRepository<Gym> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<Badge> badgeRepository,
            IRepository<Location> locationRepository,
            IRepository<Trainer> trainerRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _badgeRepository = badgeRepository;
            _locationRepository = locationRepository;
            _trainerRepository = trainerRepository;
        }

        public async Task<BadgeReturnDto> AwardBadgeAsync(AwardBadgeDto model)
        {
                var gym = await _repository.GetEntityAsync(x => x.Id == model.GymId, asNoTracking: true);
                if (gym == null)
                    throw new NotFoundException($"Gym with ID {model.GymId} not found.");
                var trainer = await _trainerRepository.GetEntityAsync(x => x.Id == model.TrainerId, asNoTracking: true);
                if (trainer == null)
                    throw new NotFoundException($"Trainer with ID {model.TrainerId} not found.");
                var badge = await _badgeRepository.GetEntityAsync(x => x.GymId == gym.Id, asNoTracking: true);
                if (badge == null)
                    throw new NotFoundException($"Badge for Gym with ID {model.GymId} not found.");
                trainer.Badges.Add(badge);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<BadgeReturnDto>(badge);
        }

        public async Task<GymReturnDto> CreateAsync(GymCreateDto model)
        {
            var locationExists = await _locationRepository.IsExistsAsync(x => x.Id == model.LocationId, asNoTracking: true);
            if (!locationExists)
                throw new NotFoundException($"Location with ID {model.LocationId} not found.");

            var gymLeaderExists = await _trainerRepository.IsExistsAsync(x => x.Id == model.GymLeaderTrainerId, asNoTracking: true);
            if (!gymLeaderExists)
                throw new NotFoundException($"Trainer with ID {model.GymLeaderTrainerId} not found.");

            var gym = _mapper.Map<Gym>(model);
            await _repository.CreateAsync(gym);

            if (model.Badge != null)
            {
                var badge = _mapper.Map<Badge>(model.Badge);
                badge.GymId = gym.Id;
                badge.Gym = gym;
                await _badgeRepository.CreateAsync(badge);
            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GymReturnDto>(gym);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");

            var entity = await _repository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException($"Gym with ID {id} not found");

            var result = await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<PagedResponse<GymListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery();
            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<GymListItemDto>>(entities);

            return new PagedResponse<GymListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<GymReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true);
            if (entity == null)
                throw new NotFoundException($"Gym with ID {id} not found");

            return _mapper.Map<GymReturnDto>(entity);
        }

        public async Task<GymReturnDto> UpdateAsync(int id, GymUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
                throw new NotFoundException($"Gym with ID {id} not found");

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GymReturnDto>(updatedEntity);
        }
    }
}
