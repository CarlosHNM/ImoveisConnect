using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Application.DTOS;

namespace ImoveisConnect.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<Result<AccountDTO>> PostLoginAsync(SignInDTO input,
            int tokenExpirationHours,
            string userByPass_HML = null);
    }
}
