using ImoveisConnect.Application.Delegates;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Interfaces.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        ValueTask<TEntity> FindByIdAsync(params object[] keyValues);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification = null);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification = null);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> LastOrDefaultAsync(ISpecification<TEntity> specification = null);

        Task<List<K>> FindTransformAsync<K>(Expression<Func<TEntity, K>> selectExpression,
            Expression<Func<TEntity, bool>> where,
            OnBeforeSelectQueryableDelegate<TEntity> onBeforeSelectQueryable = null) where K : class;

        Task<List<K>> FindTransformAsync<K>(Expression<Func<TEntity, K>> selectExpression,
            ISpecification<TEntity> specification = null,
            OnBeforeSelectQueryableDelegate<TEntity> onBeforeSelectQueryable = null) where K : class;
        Task<int> CountAsync(ISpecification<TEntity> specification = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);
        Task<bool> AnyAsync(ISpecification<TEntity> specification = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> where = null);
        Task<TEntity> MinAsync(Expression<Func<TEntity, bool>> where = null);
        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> where);
    }
}

