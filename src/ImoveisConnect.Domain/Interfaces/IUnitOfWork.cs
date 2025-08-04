using ImoveisConnect.Application.Core;
using ImoveisConnect.Application.Delegates;
using ImoveisConnect.Domain.Interfaces.IRepository;

namespace ImoveisConnect.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        int Complete();
        bool UseTransaction(OnTryBeforeSaveChangesTransaction onUsingTransaction = null, OnCatchTransaction onErrorTransaction = null);
    }
}
