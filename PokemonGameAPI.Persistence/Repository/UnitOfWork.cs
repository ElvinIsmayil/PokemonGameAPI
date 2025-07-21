using PokemonGameAPI.Domain.Repository;
using PokemonGameAPI.Persistence.Data;

namespace PokemonGameAPI.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PokemonGameDbContext _context;
        public UnitOfWork(PokemonGameDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
