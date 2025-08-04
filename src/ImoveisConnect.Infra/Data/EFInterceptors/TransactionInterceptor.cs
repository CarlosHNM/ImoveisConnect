using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace ImoveisConnect.Infra.Data.EFInterceptors
{
    public class TransactionInterceptor : DbTransactionInterceptor
    {
        public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            base.TransactionFailed(transaction, eventData);
        }

        public override Task TransactionFailedAsync(DbTransaction transaction, TransactionErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            return base.TransactionFailedAsync(transaction, eventData, cancellationToken);
        }
    }
}
