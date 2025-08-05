using AutoMapper;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class GymService : GenericService<Gym, GymRequestDto, GymResponseDto>, IGymService
    {
        private readonly IGenericRepository<Badge> _badgeRepository;
        private readonly IGenericRepository<Trainer> _trainerRepository;
        private readonly IGenericRepository<TrainerBadge> _trainerBadgeRepository;
        public GymService(IGenericRepository<Gym> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Trainer> trainerRepository, IGenericRepository<Badge> badgeRepository, IGenericRepository<TrainerBadge> trainerBadgeRepository) : base(repository, unitOfWork, mapper)
        {
            _trainerRepository = trainerRepository;
            _badgeRepository = badgeRepository;
            _trainerBadgeRepository = trainerBadgeRepository;
        }

        public async Task<BadgeResponseDto> AwardBadgeAsync(AwardBadgeDto model)
        {
            var gym = await _repository.GetEntityAsync(x => x.Id == model.GymId, asNoTracking: true);
            if (gym == null)
                throw new NotFoundException($"Gym with ID {model.GymId} not found.");
            var trainer = await _trainerRepository.GetEntityAsync(x => x.Id == model.TrainerId, asNoTracking: true);
            if (trainer == null)
                throw new NotFoundException($"Trainer with ID {model.TrainerId} not found.");
            var badge = await _badgeRepository.GetEntityAsync(x => x.GymId == model.GymId, asNoTracking: true);
            if (badge == null)
                throw new NotFoundException($"Badge for Gym with ID {model.GymId} not found.");
            var trainerBadge = await _trainerBadgeRepository.CreateAsync(new TrainerBadge
            {
                TrainerId = model.TrainerId,
                BadgeId = badge.Id
            });
            trainer.TrainerBadges.Add(trainerBadge);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BadgeResponseDto>(badge);
        }

    }
}
