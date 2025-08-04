using ImoveisConnect.Application;
using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.Services;

namespace ImoveisConnect.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _uow;

        public ClienteService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<ClienteResponseDTO>>> GetAllAsync()
        {
            var clientes = await _uow.Repository<Cliente>().FindAsync();

            if (clientes == null || !clientes.Any())
                return ResultHelper.NotFound<List<ClienteResponseDTO>>(Mensagens.NenhumRegistro);

            var dtos = clientes.Select(c => new ClienteResponseDTO(c)).ToList();
            return ResultHelper.Success(dtos);
        }

        public async Task<Result<ClienteResponseDTO>> GetByIdAsync(int id)
        {
            var cliente = await _uow.Repository<Cliente>().FirstOrDefaultAsync(x => x.ClienteId == id);

            if (cliente == null)
                return ResultHelper.Invalid<ClienteResponseDTO>(Mensagens.NenhumRegistro);

            return ResultHelper.Success(new ClienteResponseDTO(cliente));
        }

        public async Task<Result<ClienteResponseDTO>> PostAsync(ClienteRequestDTO input)
        {
            var cliente = input.GetModel();

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Cliente>().Add(cliente);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.NotFound<ClienteResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ClienteResponseDTO(cliente), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<ClienteResponseDTO>> UpdateAsync(int id, ClienteRequestDTO input)
        {
            var clienteExistente = await _uow.Repository<Cliente>().FirstOrDefaultAsync(x => x.ClienteId == id);

            if (clienteExistente == null)
                return ResultHelper.Invalid<ClienteResponseDTO>(Mensagens.NenhumRegistro);

            var clienteAtualizado = input.GetModel();
            clienteAtualizado.ClienteId = id; // garantir que o ID original seja mantido

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Cliente>().Update(clienteAtualizado);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<ClienteResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ClienteResponseDTO(clienteAtualizado), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<ClienteResponseDTO>> DeleteAsync(int id)
        {
            var cliente = await _uow.Repository<Cliente>().FirstOrDefaultAsync(x => x.ClienteId == id);

            if (cliente == null)
                return ResultHelper.NotFound<ClienteResponseDTO>(Mensagens.NenhumRegistro);

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Cliente>().Remove(cliente);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<ClienteResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ClienteResponseDTO(cliente), Mensagens.SucessoAoExcluir);
        }
    }
}
