using ImoveisConnect.Application;
using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enums;
using ImoveisConnect.Domain.Interfaces;

namespace ImoveisConnect.Domain.Services
{
    public class ApartamentoService : IApartamentoService
    {
        private readonly IUnitOfWork _uow;

        public ApartamentoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<ApartamentoResponseDTO>>> GetAllAsync()
        {
            var apartamentos = await _uow.Repository<Apartamento>().FindAsync();

            if (apartamentos == null || !apartamentos.Any())
                return ResultHelper.NotFound<List<ApartamentoResponseDTO>>(Mensagens.NenhumRegistro);

            var dtos = apartamentos.Select(x => new ApartamentoResponseDTO(x)).ToList();
            return ResultHelper.Success(dtos);
        }

        public async Task<Result<ApartamentoResponseDTO>> GetByIdAsync(string codigoApartamento)
        {
            var model = await _uow.Repository<Apartamento>().FirstOrDefaultAsync(x => x.CodigoApartamento == codigoApartamento);

            if (model == null)
                return ResultHelper.NotFound<ApartamentoResponseDTO>(Mensagens.NenhumRegistro);

            return ResultHelper.Success(new ApartamentoResponseDTO(model));
        }

        public async Task<Result<ApartamentoResponseDTO>> PostAsync(ApartamentoRequestDTO dto)
        {
            var model = dto.GetModel();

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Apartamento>().Add(model);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<ApartamentoResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ApartamentoResponseDTO(model), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<ApartamentoResponseDTO>> UpdateAsync(string codigoApartamento, ApartamentoRequestDTO dto)
        {
            var existente = await _uow.Repository<Apartamento>().FirstOrDefaultAsync(x => x.CodigoApartamento == codigoApartamento);

            if (existente == null)
                return ResultHelper.NotFound<ApartamentoResponseDTO>(Mensagens.NenhumRegistro);

            existente.CodigoApartamento = dto.CodigoApartamento;
            existente.Endereco = dto.Endereco;
            existente.Preco = dto.Preco;
            existente.Descricao = dto.Descricao;
            existente.NumeroQuartos = dto.NumeroQuartos;
            existente.Area = dto.Area;
            existente.DataUltimaAtualizacaoStatus = DateTime.UtcNow;

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Apartamento>().Update(existente);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<ApartamentoResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ApartamentoResponseDTO(existente), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<ApartamentoResponseDTO>> DeleteAsync(string codigoApartamento)
        {
            var model = await _uow.Repository<Apartamento>().FirstOrDefaultAsync(x => x.CodigoApartamento == codigoApartamento);

            if (model == null)
                return ResultHelper.NotFound<ApartamentoResponseDTO>(Mensagens.NenhumRegistro);

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Apartamento>().Remove(model);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<ApartamentoResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new ApartamentoResponseDTO(model), Mensagens.SucessoAoExcluir);
        }

        public async Task<Result<bool>> VerificarDisponibilidadeAsync(string codigoApartamento)
        {
            var model = await _uow.Repository<Apartamento>().FirstOrDefaultAsync(x => x.CodigoApartamento == codigoApartamento);

            if (model == null)
                return ResultHelper.NotFound<bool>(Mensagens.NenhumRegistro);

            var disponivel = model.Status == DisponibilidadeStatus.Disponivel;
            return ResultHelper.Success(disponivel);
        }
    }
}
