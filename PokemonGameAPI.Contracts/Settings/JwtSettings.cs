﻿namespace PokemonGameAPI.Contracts.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string SecretKey { get; set; } = default!;
    }
}
