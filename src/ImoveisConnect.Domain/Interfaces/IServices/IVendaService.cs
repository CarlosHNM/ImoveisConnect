using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;

namespace ImoveisConnect.Domain.Interfaces.Services
{
    public interface IVendaService
    {
        /// <summary>
        /// Retorna todas as vendas realizadas no sistema.
        /// </summary>
        /// <returns>Lista de vendas encapsulada em um resultado.</returns>
        Task<Result<List<VendaResponseDTO>>> GetAllAsync();

        /// <summary>
        /// Retorna os dados de uma venda específica a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador único da venda.</param>
        /// <returns>Venda correspondente ao ID informado, caso exista.</returns>
        Task<Result<VendaResponseDTO>> GetByIdAsync(int id);

        /// <summary>
        /// Atualiza o status de pagamento de uma venda existente.
        /// </summary>
        /// <param name="input">Dados contendo ID da venda e novo status de pagamento.</param>
        /// <returns>Venda atualizada ou erro em caso de falha.</returns>
        Task<Result<VendaResponseDTO>> AtualizarStatusPagamentoAsync(VendaRequestDTO input);

        /// <summary>
        /// Realiza o processo completo de venda: valida a disponibilidade do apartamento,
        /// ausência de reserva ativa, e efetua a associação da venda ao cliente.
        /// </summary>
        /// <param name="input">Dados da venda incluindo cliente, apartamento, observações e valor de entrada.</param>
        /// <returns>Dados da reserva convertida em venda, com status atualizado.</returns>
        Task<Result<VendaResponseDTO>> RealizarVendaAsync(VendaRequestDTO input);

        /// <summary>
        /// Realiza uma reserva para um apartamento, verificando se ele está disponível e sem reserva ativa.
        /// </summary>
        /// <param name="input">Dados da reserva, incluindo cliente e apartamento.</param>
        /// <returns>Resultado da operação com os dados da venda associada à reserva.</returns>
        Task<Result<ReservaResponseDTO>> RealizarReservaAsync(VendaRequestDTO input);

        /// <summary>
        /// Cancela a reserva ativa de um apartamento com base no código.
        /// </summary>
        /// <param name="codigoApartamento">Código do apartamento.</param>
        Task<Result<string>> CancelarReservaAsync(string codigoApartamento);

    }
}
