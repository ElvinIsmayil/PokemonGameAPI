namespace PokemonGameAPI.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string secretKey { get; set; } = default!;
    }
}
