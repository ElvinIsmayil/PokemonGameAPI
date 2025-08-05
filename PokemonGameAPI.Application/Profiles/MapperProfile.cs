using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PokemonGameAPI.Contracts.DTOs.Auth;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Gym;
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
using PokemonGameAPI.Domain.Enum;

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
         // Map the single BattlePokemons collection and then split/filter them inside the DTO if needed
         .ForMember(dest => dest.Trainer1BattlePokemons, opt => opt.MapFrom(src => src.BattlePokemons.Where(bp => bp.Side == TrainerSide.Trainer1)))
         .ForMember(dest => dest.Trainer2BattlePokemons, opt => opt.MapFrom(src => src.BattlePokemons.Where(bp => bp.Side == TrainerSide.Trainer2)));

            CreateMap<Battle, BattleListItemDto>();

            CreateMap<BattleCreateDto, Battle>()
                .ForMember(dest => dest.BattlePokemons, opt => opt.Ignore());

            CreateMap<BattleUpdateDto, Battle>()
                .ForMember(dest => dest.BattlePokemons, opt => opt.Ignore());


            // Gym
            CreateMap<Gym, GymReturnDto>();
            CreateMap<Gym, GymListItemDto>();
            CreateMap<GymCreateDto, Gym>().ReverseMap();
            CreateMap<GymUpdateDto, Gym>().ReverseMap();

            // Pokemon
            CreateMap<Pokemon, PokemonReturnDto>()
            .ForMember(dest => dest.PokemonCategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Abilities, opt => opt.MapFrom(src => src.Abilities))
            .ForMember(dest => dest.BaseStats, opt => opt.MapFrom(src => src.BaseStats))
            .ForMember(dest => dest.TrainerPokemons, opt => opt.MapFrom(src => src.TrainerPokemons));

            CreateMap<Pokemon, PokemonListItemDto>()
                .ForMember(dest => dest.PokemonCategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<PokemonCreateDto, Pokemon>().ReverseMap();
            CreateMap<PokemonUpdateDto, Pokemon>().ReverseMap();

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

            CreateMap<PokemonStats, TrainerPokemonStats>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore())
                .ReverseMap();


            // Role
            CreateMap<IdentityRole, RoleListItemDto>();
            CreateMap<IdentityRole, RoleReturnDto>();
            CreateMap<RoleCreateDto, IdentityRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is generated by the database
                .ReverseMap();

            // Tournament
            CreateMap<Tournament, TournamentReturnDto>()
                .ForMember(dest => dest.WinnerName, opt => opt.MapFrom(src => src.Winner != null ? src.Winner.Name : string.Empty));
            CreateMap<Tournament, TournamentListItemDto>()
                .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.Participants.Count))
                .ForMember(dest => dest.BattleCount, opt => opt.MapFrom(src => src.Battles.Count));
            CreateMap<TournamentCreateDto, Tournament>().ReverseMap();
            CreateMap<TournamentUpdateDto, Tournament>()
                .ReverseMap();

            // Trainer
            CreateMap<Trainer, TrainerReturnDto>()
                .ForMember(dest => dest.TrainerPokemons, opt => opt.MapFrom(src => src.TrainerPokemons))
                .ForMember(dest => dest.Badges, opt => opt.MapFrom(src => src.TrainerBadges))
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
                .ForMember(dest => dest.TrainerPokemonStats, opt => opt.MapFrom(src => src.TrainerPokemonStats));

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

            // TrinerPokemonStats to PokemonStats mapping
            CreateMap<PokemonStatsReturnDto, TrainerPokemonStatsReturnDto>();
            CreateMap<PokemonStatsListItemDto, TrainerPokemonStatsListItemDto>();
            CreateMap<PokemonStatsCreateDto, TrainerPokemonStatsCreateDto>()
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore()) // handled in service
                .ReverseMap();
            CreateMap<PokemonStatsUpdateDto, TrainerPokemonStatsUpdateDto>()
                .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore()) // handled in service
                .ReverseMap();

            // PokemonStats to TrainerPokemonStats mapping
            CreateMap<PokemonStatsListItemDto, TrainerPokemonStats>()
    .ForMember(dest => dest.Id, opt => opt.Ignore()) // ehtiyac varsa
    .ForMember(dest => dest.TrainerPokemonId, opt => opt.Ignore()); // lazımdırsa


        }
    }
}
