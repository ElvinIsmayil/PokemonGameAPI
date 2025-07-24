namespace PokemonGameAPI.Contracts.DTOs.Gym
{
    public record GymReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string LocationName { get; set; } = default!;
        public string BadgeName { get; set; } = default!;
        public string GymLeaderName { get; set; } = default!;
    }
}
