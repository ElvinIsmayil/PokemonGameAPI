namespace PokemonGameAPI.Contracts.DTOs.Pagination
{
    public record PagedResponse<TEntity>
    {
        public List<TEntity> Data { get; set; } = new List<TEntity>();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
