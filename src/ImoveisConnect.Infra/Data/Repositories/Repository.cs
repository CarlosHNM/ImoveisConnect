using ImoveisConnect.Application.Core;
using ImoveisConnect.Application.Delegates;
using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ImoveisConnect.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().AddRangeAsync(entities);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public void Remove(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public TEntity FindById(params object[] keyValues)
        //{
        //    var entity = _context.Set<TEntity>().Find(keyValues);
        //    if(entity != null) _context.Entry(entity).State = EntityState.Detached;
        //    return entity;
        //}

        public ValueTask<TEntity> FindByIdAsync(params object[] keyValues)
        {
            try
            {
                var entity = _context.Set<TEntity>().FindAsync(keyValues);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where)
        //{
        //    return _context.Set<TEntity>().Where(where);
        //}

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _context.Set<TEntity>().Where(where).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                //return _context.Set<TEntity>().Async
                return _context.Set<TEntity>().Where(where).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification = null)
        {
            try
            {
                //return _context.Set<TEntity>().Async
                var query = ApplySpecification(specification);
                return query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                //return _context.Set<TEntity>().Async
                return _context.Set<TEntity>().Where(where).OrderDescending().FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> LastOrDefaultAsync(ISpecification<TEntity> specification = null)
        {
            try
            {
                //return _context.Set<TEntity>().Async
                var query = ApplySpecification(specification);
                return query.OrderDescending().FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification = null)
        {
            try
            {
                return ApplySpecification(specification).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null)
        //{
        //    return ApplySpecification(specification);
        //}

        //public IQueryable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, Expression<Func<TEntity, bool>> where) where K : class
        //{
        //    return _context.Set<TEntity>().Where(where).Select(selectExpression);
        //}

        //public IQueryable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, ISpecification<TEntity> specification = null) where K : class
        //{
        //    return ApplySpecification(specification).Select(selectExpression);
        //}

        #region Métodos Mágicos

        public Task<List<K>> FindTransformAsync<K>(Expression<Func<TEntity, K>> selectExpression,
            Expression<Func<TEntity, bool>> where,
            OnBeforeSelectQueryableDelegate<TEntity> onBeforeSelectQueryable = null) where K : class
        {
            try
            {
                var query = _context.Set<TEntity>().Where(where).AsQueryable();
                if (onBeforeSelectQueryable != null)
                {
                    onBeforeSelectQueryable(ref query);
                }

                var select = query.Select(selectExpression);

                return select.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<List<K>> FindTransformAsync<K>(Expression<Func<TEntity, K>> selectExpression,
            ISpecification<TEntity> specification = null,
            OnBeforeSelectQueryableDelegate<TEntity> onBeforeSelectQueryable = null) where K : class
        {
            try
            {
                var query = ApplySpecification(specification);
                if (onBeforeSelectQueryable != null)
                {
                    onBeforeSelectQueryable(ref query);
                }

                var select = query.Select(selectExpression);

                return select.ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        #endregion

        //public int Count(ISpecification<TEntity> specification = null)
        //{
        //    return ApplySpecification(specification).Count();
        //}

        public Task<int> CountAsync(ISpecification<TEntity> specification = null)
        {
            try
            {
                return ApplySpecification(specification).CountAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public int Count(Expression<Func<TEntity, bool>> where)
        //{
        //    return _context.Set<TEntity>().Count(where);
        //}

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _context.Set<TEntity>().CountAsync(where);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public bool Any(ISpecification<TEntity> specification = null)
        //{
        //    return ApplySpecification(specification).Any();
        //}

        public Task<bool> AnyAsync(ISpecification<TEntity> specification = null)
        {
            try
            {
                return ApplySpecification(specification).AnyAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        //public bool Any(Expression<Func<TEntity, bool>> where)
        //{
        //    return _context.Set<TEntity>().Any(where);
        //}
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _context.Set<TEntity>().AnyAsync(where);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<bool> AllAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _context.Set<TEntity>().AllAsync(where);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> MaxAsync(Expression<Func<TEntity, bool>> where = null)
        {
            try
            {
                var query = _context.Set<TEntity>().AsQueryable();
                if (where != null)
                    query = query.Where(where);

                return query.MaxAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<TEntity> MinAsync(Expression<Func<TEntity, bool>> where = null)
        {
            try
            {
                var query = _context.Set<TEntity>().AsQueryable();
                if (where != null)
                    query = query.Where(where);

                return query.MinAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

        }

        public Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> where)
        {
            try
            {
                return _context.Set<TEntity>().SumAsync(where);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            try
            {
                return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
