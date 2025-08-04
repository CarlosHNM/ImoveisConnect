using ImoveisConnect.API.Controllers.Core;
using ImoveisConnect.API.Core.Extensions;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImoveisConnect.API.Controllers
{
    [ControllerName("clientes")]
    [Authorize]
    [Route("api/[controller]")]
    public class ClienteController : BaseApiController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Buscar todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clienteService.GetAllAsync();
            return this.FromResult(result); ;
        }

        /// <summary>
        /// Buscar cliente por ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _clienteService.GetByIdAsync(id);
            return this.FromResult(result);
        }

        /// <summary>
        /// Cadastrar novo cliente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteRequestDTO dto)
        {
            var result = await _clienteService.PostAsync(dto);
            return this.FromResult(result);
        }

        /// <summary>
        /// Atualizar cliente existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteRequestDTO dto)
        {
            var result = await _clienteService.UpdateAsync(id, dto);
            return this.FromResult(result);
        }

        /// <summary>
        /// Remover cliente.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteService.DeleteAsync(id);
            return this.FromResult(result);
        }
    }
}
