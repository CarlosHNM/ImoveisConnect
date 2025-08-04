using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;

public interface IApartamentoService
{
    Task<Result<List<ApartamentoResponseDTO>>> GetAllAsync();
    Task<Result<ApartamentoResponseDTO>> GetByIdAsync(string codigoApartamento);
    Task<Result<ApartamentoResponseDTO>> PostAsync(ApartamentoRequestDTO dto);
    Task<Result<ApartamentoResponseDTO>> UpdateAsync(string codigoApartamento, ApartamentoRequestDTO dto);
    Task<Result<ApartamentoResponseDTO>> DeleteAsync(string codigoApartamento);
    Task<Result<bool>> VerificarDisponibilidadeAsync(string codigoApartamento);
}
