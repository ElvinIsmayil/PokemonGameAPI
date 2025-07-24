using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.DTOs.Location;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.DTOs.Tournament;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Badge
            CreateMap<Badge, BadgeReturnDto>();
            CreateMap<Badge, BadgeListItemDto>();
            CreateMap<BadgeCreateDto, Badge>().ReverseMap();
            CreateMap<BadgeUpdateDto, Badge>().ReverseMap();

            // Battle
            CreateMap<Battle, BattleReturnDto>()
                .ForMember(dest => dest.Trainer1Name, opt => opt.MapFrom(src => src.Trainer1.Name))
                .ForMember(dest => dest.Trainer2Name, opt => opt.MapFrom(src => src.Trainer2.Name));

            // Pokemon Category
            CreateMap<PokemonCategory, PokemonCategoryReturnDto>();
            CreateMap<PokemonCategory, PokemonCategoryListItemDto>()
                .ForMember(dest => dest.PokemonCount, opt => opt.MapFrom(src => src.Pokemons.Count));
            CreateMap<PokemonCategoryUpdateDto, PokemonCategory>().ReverseMap();
            CreateMap<PokemonCategoryCreateDto, PokemonCategory>().ReverseMap();

            // Gym
            CreateMap<Gym, GymReturnDto>();
            CreateMap<Gym, GymListItemDto>()
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));
            CreateMap<GymCreateDto, Gym>()
                .ForMember(dest => dest.LocationId, opt => opt.Ignore()) // optional: handle in service layer
                .ReverseMap();
            CreateMap<GymUpdateDto, Gym>().ReverseMap();

            // Location
            CreateMap<Location, LocationReturnDto>()
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



        }
    }
}
