using ImoveisConnect.API.Controllers.Core;
using ImoveisConnect.API.Core.Extensions;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImoveisConnect.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class VendaController : BaseApiController
    {
        private readonly IVendaService _venda;

        public VendaController(IVendaService venda)
        {
            _venda = venda;
        }

        /// <summary>
        /// Realiza a venda de um apartamento.
        /// </summary>
        [HttpPost("realizar-venda")]
        public async Task<IActionResult> RealizarVenda([FromBody] VendaRequestDTO input)
        {
            var result = await _venda.RealizarVendaAsync(input);
            return this.FromResult(result);
        }

        /// <summary>
        /// Realiza a reserva de um apartamento.
        /// </summary>
        [HttpPost("realizar-reserva")]
        public async Task<IActionResult> RealizarReserva([FromBody] VendaRequestDTO input)
        {
            var result = await _venda.RealizarReservaAsync(input);
            return this.FromResult(result);
        }

        /// <summary>
        /// Atualiza o status de pagamento da venda.
        /// </summary>
        [HttpPut("atualizar-status-pagamento")]
        public async Task<IActionResult> AtualizarStatusPagamento([FromBody] VendaRequestDTO input)
        {
            var result = await _venda.AtualizarStatusPagamentoAsync(input);
            return this.FromResult(result);
        }

        /// <summary>
        /// Retorna todas as vendas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _venda.GetAllAsync();
            return this.FromResult(result);
        }

        /// <summary>
        /// Retorna os dados de uma venda específica.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _venda.GetByIdAsync(id);
            return this.FromResult(result);
        }

        /// <summary>
        /// Cancela a reserva ativa de um apartamento.
        /// </summary>
        /// <param name="codigoApartamento">Código do apartamento</param>
        [HttpPost("cancelar-reserva/{codigoApartamento}")]
        public async Task<IActionResult> CancelarReserva(string codigoApartamento)
        {
            var resultado = await _venda.CancelarReservaAsync(codigoApartamento);
            return this.FromResult(resultado);
        }

    }
}
