using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace ImoveisConnect.Infra.Data.EFInterceptors
{
    public class ConnectionInterceptor : DbCommandInterceptor
    {
        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            base.CommandFailed(command, eventData);
        }

        public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            return base.CommandFailedAsync(command, eventData, cancellationToken);
        }
    }
}
