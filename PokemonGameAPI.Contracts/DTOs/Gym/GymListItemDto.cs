namespace PokemonGameAPI.Contracts.DTOs.Gym
{
    public record GymListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string LocationName { get; set; } = default!;
    }
}
