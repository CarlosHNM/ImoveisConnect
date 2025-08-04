using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;

namespace ImoveisConnect.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Result<List<ClienteResponseDTO>>> GetAllAsync();
        Task<Result<ClienteResponseDTO>> GetByIdAsync(int id);
        Task<Result<ClienteResponseDTO>> PostAsync(ClienteRequestDTO input);
        Task<Result<ClienteResponseDTO>> UpdateAsync(int id, ClienteRequestDTO dto);
        Task<Result<ClienteResponseDTO>> DeleteAsync(int id);
    }
}
