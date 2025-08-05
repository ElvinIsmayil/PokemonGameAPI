using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IGenericService<TEntity, TRequest, TResponse>
    {
        Task<PagedResponse<TResponse>> GetAllAsync(int pageNumber, int pageSize, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);
        Task<TResponse> GetByIdAsync(int id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);
        Task<TResponse> CreateAsync(TRequest request);
        Task<TResponse> UpdateAsync(int id, TRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
