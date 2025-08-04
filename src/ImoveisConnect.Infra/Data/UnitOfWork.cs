using ImoveisConnect.Application.Core;
using ImoveisConnect.Application.Delegates;
using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.IRepository;
using ImoveisConnect.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace ImoveisConnect.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public bool UseTransaction(OnTryBeforeSaveChangesTransaction onUsingTransaction = null, OnCatchTransaction onErrorTransaction = null)
        {
            if (onUsingTransaction == null)
                return false;

            IDbContextTransaction transaction = null;
            using (transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    if (onUsingTransaction != null)
                        onUsingTransaction();
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    if (onErrorTransaction != null)
                        onErrorTransaction();

                    if (transaction != null)
                        transaction.Rollback();
                    return false;
                }
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
