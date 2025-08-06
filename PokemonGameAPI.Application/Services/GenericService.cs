using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.CustomExceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class GenericService<TEntity, TRequest, TResponse> : IGenericService<TEntity, TRequest, TResponse>
        where TEntity : BaseEntity, new()
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TResponse> CreateAsync(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TResponse>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new CustomException($"Entity with ID {id} not found.");
            }

            var result = await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            if (!result)
            {
                throw new CustomException($"Failed to delete entity with ID {id}.");
            }
            return result;
        }

        public virtual async Task<PagedResponse<TResponse>> GetAllAsync(int pageNumber, int pageSize, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var query = _repository.GetQuery(includes: includes);

            var totalCount = await query.CountAsync();
            var skip = (pageNumber - 1) * pageSize;

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<TResponse>>(entities);

            return new PagedResponse<TResponse>()
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public virtual async Task<TResponse> GetByIdAsync(int id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true, includes: includes);
            if (entity == null)
            {
                throw new CustomException($"Entity with ID {id} not found.");
            }
            return _mapper.Map<TResponse>(entity);

        }

        public async Task<TResponse> UpdateAsync(int id, TRequest request)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new CustomException($"Entity with ID {id} not found.");
            }

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TResponse>(entity);

        }
    }
}
