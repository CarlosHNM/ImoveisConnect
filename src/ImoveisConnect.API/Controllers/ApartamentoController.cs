using ImoveisConnect.API.Controllers.Core;
using ImoveisConnect.API.Core.Extensions;
using ImoveisConnect.Domain.DTOS.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImoveisConnect.API.Controllers
{
    [ControllerName("apartamentos")]
    [Authorize]
    [Route("api/[controller]")]
    public class ApartamentoController : BaseApiController
    {
        private readonly IApartamentoService _apartamentoService;
        public ApartamentoController(IApartamentoService apartamentoService)
        {
            _apartamentoService = apartamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _apartamentoService.GetAllAsync();
            return this.FromResult(result);
        }

        [HttpGet("{codigoApartamento}")]
        public async Task<IActionResult> GetById([FromRoute] string codigoApartamento)
        {
            var result = await _apartamentoService.GetByIdAsync(codigoApartamento);
            return this.FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApartamentoRequestDTO dto)
        {
            var result = await _apartamentoService.PostAsync(dto);
            return this.FromResult(result);
        }

        [HttpPut("{codigoApartamento}")]
        public async Task<IActionResult> Put([FromRoute] string codigoApartamento, [FromBody] ApartamentoRequestDTO dto)
        {
            var result = await _apartamentoService.UpdateAsync(codigoApartamento, dto);
            return this.FromResult(result);
        }

        [HttpDelete("{codigoApartamento}")]
        public async Task<IActionResult> Delete([FromRoute] string codigoApartamento)
        {
            var result = await _apartamentoService.DeleteAsync(codigoApartamento);
            return this.FromResult(result);
        }

        [HttpGet("verificar-disponibilidade/{codigoApartamento}")]
        public async Task<IActionResult> VerificarDisponibilidade([FromRoute] string codigoApartamento)
        {
            var result = await _apartamentoService.VerificarDisponibilidadeAsync(codigoApartamento);
            return this.FromResult(result);
        }
    }
}
