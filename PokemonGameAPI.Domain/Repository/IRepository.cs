using PokemonGameAPI.Domain.Entities.Common;
using System.Linq.Expressions;

namespace PokemonGameAPI.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<TEntity?> GetEntityAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> IsExistsAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool isIgnoredDeleteBehaviour = false);

        IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes);
    }
}
