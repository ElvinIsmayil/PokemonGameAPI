namespace PokemonGameAPI.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

    }
}
