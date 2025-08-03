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
            try
            {
                await _table.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating entity: " + ex.Message, ex);
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _table.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                entity.IsDeleted = true;
                _table.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting entity: " + ex.Message, ex);
            }
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
            try
            {
                IQueryable<TEntity> query = _table;

                if (includes is { Length: > 0 })
                {
                    foreach (var include in includes)
                        query = include(query);
                }

                if (predicate != null)
                    query = query.Where(predicate);

                if (skip > 0)
                    query = query.Skip(skip);

                if (take > 0)
                    query = query.Take(take);

                if (asNoTracking)
                    query = query.AsNoTracking();

                if (asSplitQuery)
                    query = query.AsSplitQuery();

                if (isIgnoredDeleteBehaviour)
                    query = query.IgnoreQueryFilters();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllAsync: " + ex.Message, ex);
            }
        }

        public async Task<TEntity?> GetEntityAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
        {
            try
            {
                IQueryable<TEntity> query = _table;

                if (includes is { Length: > 0 })
                {
                    foreach (var include in includes)
                        query = include(query);
                }

                if (predicate != null)
                    query = query.Where(predicate);

                if (skip > 0)
                    query = query.Skip(skip);

                if (take > 0)
                    query = query.Take(take);

                if (asNoTracking)
                    query = query.AsNoTracking();

                if (asSplitQuery)
                    query = query.AsSplitQuery();

                if (isIgnoredDeleteBehaviour)
                    query = query.IgnoreQueryFilters();

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEntityAsync: " + ex.Message, ex);
            }
        }

        public IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
        {
            try
            {
                IQueryable<TEntity> query = _table;

                if (includes is { Length: > 0 })
                {
                    foreach (var include in includes)
                        query = include(query);
                }

                if (predicate != null)
                    query = query.Where(predicate);

                if (asNoTracking)
                    query = query.AsNoTracking();

                if (asSplitQuery)
                    query = query.AsSplitQuery();

                if (isIgnoredDeleteBehaviour)
                    query = query.IgnoreQueryFilters();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetQuery for type {typeof(TEntity).Name}: {ex.Message}", ex);
            }
        }

        public async Task<bool> IsExistsAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool asNoTracking = false,
            bool isIgnoredDeleteBehaviour = false)
        {
            try
            {
                IQueryable<TEntity> query = _table;

                if (asNoTracking)
                    query = query.AsNoTracking();

                if (isIgnoredDeleteBehaviour)
                    query = query.IgnoreQueryFilters();

                return predicate != null && await query.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in IsExistsAsync: " + ex.Message, ex);
            }
        }
    }
}
