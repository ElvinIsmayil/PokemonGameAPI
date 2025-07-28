using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PokemonGameAPI.Contracts.DTOs.Auth;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.DTOs.Location;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.DTOs.Role;
using PokemonGameAPI.Contracts.DTOs.Tournament;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;
using PokemonGameAPI.Contracts.DTOs.User;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Auth
            CreateMap<AppUser, RegisterDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // Password handled in service
                .ReverseMap();

            // Badge
            CreateMap<Badge, BadgeReturnDto>();
            CreateMap<Badge, BadgeListItemDto>();
            CreateMap<BadgeCreateDto, Badge>().ReverseMap();
            CreateMap<BadgeUpdateDto, Badge>().ReverseMap();

            // Battle
            CreateMap<Battle, BattleReturnDto>()
                .ForMember(dest => dest.Trainer1Name, opt => opt.MapFrom(src => src.Trainer1.Name))
                .ForMember(dest => dest.Trainer2Name, opt => opt.MapFrom(src => src.Trainer2.Name))
                .ForMember(dest => dest.WinnerName, opt => opt.MapFrom(src => src.Winner != null ? src.Winner.Name : string.Empty));
            CreateMap<Battle, BattleListItemDto>();
            CreateMap<BattleCreateDto, Battle>()
                .ForMember(dest => dest.Trainer1Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainer2Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<BattleUpdateDto, Battle>()
                .ForMember(dest => dest.Trainer1Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainer2Id, opt => opt.Ignore())
                .ReverseMap();

            // Gym
            CreateMap<Gym, GymReturnDto>();
            CreateMap<Gym, GymListItemDto>()
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));
            CreateMap<GymCreateDto, Gym>()
                .ForMember(dest => dest.LocationId, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GymUpdateDto, Gym>().ReverseMap();

            // Location
            CreateMap<Location, LocationReturnDto>()
                // Assuming you want to map collections of related DTOs, 
                // but projecting them like this may fail in EF Core queries, be cautious.
                .ForMember(dest => dest.WildPokemonIds, opt => opt.MapFrom(src => src.WildPokemons.Select(p => new PokemonListItemDto { Id = p.Id, Name = p.Name })))
                .ForMember(dest => dest.GymIds, opt => opt.MapFrom(src => src.Gyms.Select(g => new GymListItemDto { Id = g.Id, Name = g.Name })))
                .ForMember(dest => dest.TournamentIds, opt => opt.MapFrom(src => src.Tournaments.Select(t => new TournamentListItemDto { Id = t.Id, Name = t.Name })));

            CreateMap<Location, LocationListItemDto>()
                .ForMember(dest => dest.WildPokemonCount, opt => opt.MapFrom(src => src.WildPokemons.Count))
                .ForMember(dest => dest.GymCount, opt => opt.MapFrom(src => src.Gyms.Count))
                .ForMember(dest => dest.TournamentCount, opt => opt.MapFrom(src => src.Tournaments.Count));
            CreateMap<LocationCreateDto, Location>()
                .ForMember(dest => dest.WildPokemons, opt => opt.Ignore())
                .ForMember(dest => dest.Gyms, opt => opt.Ignore())
                .ForMember(dest => dest.Tournaments, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<LocationUpdateDto, Location>().ReverseMap();

            // Pokemon Category
            CreateMap<PokemonCategory, PokemonCategoryReturnDto>();
            CreateMap<PokemonCategory, PokemonCategoryListItemDto>()
                .ForMember(dest => dest.PokemonCount, opt => opt.MapFrom(src => src.Pokemons.Count));
            CreateMap<PokemonCategoryUpdateDto, PokemonCategory>().ReverseMap();
            CreateMap<PokemonCategoryCreateDto, PokemonCategory>().ReverseMap();

            // Pokemon Ability
            CreateMap<PokemonAbility, PokemonAbilityReturnDto>();
            CreateMap<PokemonAbility, PokemonAbilityListItemDto>()
                .ForMember(dest => dest.PokemonCount, opt => opt.MapFrom(src => src.Pokemons.Count));
            CreateMap<PokemonAbilityCreateDto, PokemonAbility>().ReverseMap();
            CreateMap<PokemonAbilityUpdateDto, PokemonAbility>().ReverseMap();

            // Pokemon Stats
            CreateMap<PokemonStats, PokemonStatsCreateDto>().ReverseMap();
            CreateMap<PokemonStats, PokemonStatsUpdateDto>().ReverseMap();
            CreateMap<PokemonStats, PokemonStatsReturnDto>()
                .ForMember(dest => dest.PokemonName, opt => opt.MapFrom(src => src.Pokemon.Name));
            CreateMap<PokemonStats, PokemonStatsListItemDto>();

            CreateMap<PokemonStats,TrainerPokemonStats>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore()) // 
                .ReverseMap();


            // Role
            CreateMap<IdentityRole, RoleListItemDto>();
            CreateMap<IdentityRole, RoleReturnDto>();
            CreateMap<RoleCreateDto, IdentityRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is generated by the database
                .ReverseMap();

            // Tournament
            CreateMap<Tournament, TournamentReturnDto>()
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest => dest.WinnerName, opt => opt.MapFrom(src => src.Winner != null ? src.Winner.Name : string.Empty));
            CreateMap<Tournament, TournamentListItemDto>()
                .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.Participants.Count))
                .ForMember(dest => dest.BattleCount, opt => opt.MapFrom(src => src.Battles.Count));
            CreateMap<TournamentCreateDto, Tournament>().ReverseMap();
            CreateMap<TournamentUpdateDto, Tournament>()
                .ForMember(dest => dest.LocationId, opt => opt.Ignore())
                .ReverseMap();

            // Trainer
            CreateMap<Trainer, TrainerReturnDto>()
                .ForMember(dest => dest.TrainerPokemons, opt => opt.MapFrom(src => src.TrainerPokemons))
                .ForMember(dest => dest.Badges, opt => opt.MapFrom(src => src.Badges))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.UserName));

            CreateMap<Trainer, TrainerListItemDto>()
                .ForMember(dest => dest.TournamentCount, opt => opt.MapFrom(src => src.Tournaments.Count))
                .ForMember(dest => dest.BattleCount, opt => opt.MapFrom(src => src.BattlesAsTrainer1.Count + src.BattlesAsTrainer2.Count))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.Name));

            CreateMap<TrainerCreateDto, Trainer>().ReverseMap();

            CreateMap<TrainerUpdateDto, Trainer>()
                .ForMember(dest => dest.Tournaments, opt => opt.Ignore())
                .ForMember(dest => dest.BattlesAsTrainer1, opt => opt.Ignore())
                .ForMember(dest => dest.BattlesAsTrainer2, opt => opt.Ignore())
                .ReverseMap();

            // Trainer Pokemon
            CreateMap<TrainerPokemon, TrainerPokemonReturnDto>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.PokemonName, opt => opt.MapFrom(src => src.Pokemon.Name))
                .ForMember(dest => dest.TrainerPokemonStatsDto, opt => opt.MapFrom(src => src.TrainerPokemonStats));

            CreateMap<TrainerPokemon, TrainerPokemonListItemDto>()
                .ForMember(dest => dest.PokemonName, opt => opt.MapFrom(src => src.Pokemon.Name));
            CreateMap<TrainerPokemonCreateDto, TrainerPokemon>().ReverseMap();
            CreateMap<TrainerPokemonUpdateDto, TrainerPokemon>()
                .ForMember(dest => dest.PokemonId, opt => opt.Ignore()) // handled manually
                .ReverseMap();

            // Trainer Pokemon Stats
            CreateMap<TrainerPokemonStats, TrainerPokemonStatsReturnDto>();
            CreateMap<TrainerPokemonStats, TrainerPokemonStatsListItemDto>();
            CreateMap<TrainerPokemonStatsCreateDto, TrainerPokemonStats>()
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore()) // handled in service
                .ReverseMap();
            CreateMap<TrainerPokemonStatsUpdateDto, TrainerPokemonStats>()
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore())
                .ReverseMap();

            // AppUser
            CreateMap<AppUser, UserReturnDto>();
            CreateMap<AppUser, UserListItemDto>();
            CreateMap<UserUpdateDto, AppUser>().ReverseMap();
        }
    }
}
