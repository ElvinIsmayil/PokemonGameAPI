using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Domain.Repository;
using PokemonGameAPI.Persistence.Data;
using System.Linq.Expressions;

namespace PokemonGameAPI.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly PokemonGameDbContext _context;
        protected readonly DbSet<TEntity> _table;

        public Repository(PokemonGameDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _table.Update(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            _table.Update(entity);
            return Task.FromResult(true);
        }

        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
        {
            var query = BuildQuery(predicate, asNoTracking, asSplitQuery, isIgnoredDeleteBehaviour, includes);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetEntityAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
        {
            var query = BuildQuery(predicate, asNoTracking, asSplitQuery, isIgnoredDeleteBehaviour, includes);

            if (skip > 0)
                query = query.Skip(skip);

            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
        {
            return BuildQuery(predicate, asNoTracking, asSplitQuery, isIgnoredDeleteBehaviour, includes);
        }

        public async Task<bool> IsExistsAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool isIgnoredDeleteBehaviour = false)
        {
            if (predicate == null)
                return false;

            var query = BuildQuery(predicate, asNoTracking, false, isIgnoredDeleteBehaviour);
            return await query.AnyAsync();
        }

        private IQueryable<TEntity> BuildQuery(
            Expression<Func<TEntity, bool>>? predicate,
            bool asNoTracking,
            bool asSplitQuery,
            bool isIgnoredDeleteBehaviour,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes = null)
        {
            IQueryable<TEntity> query = _table;

            if (isIgnoredDeleteBehaviour)
                query = query.IgnoreQueryFilters();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (asSplitQuery)
                query = query.AsSplitQuery();

            if (includes is { Length: > 0 })
            {
                foreach (var include in includes)
                    query = include(query);
            }

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }
    }
}
