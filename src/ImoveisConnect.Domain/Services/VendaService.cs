using ImoveisConnect.Application;
using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Domain.DTOS.Request;
using ImoveisConnect.Domain.DTOS.Response;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enum;
using ImoveisConnect.Domain.Enums;
using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.Services;
using ImoveisConnect.Domain.Specification;

namespace ImoveisConnect.Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IUnitOfWork _uow;

        public VendaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<VendaResponseDTO>>> GetAllAsync()
        {
            var vendas = await _uow.Repository<Venda>().FindAsync();

            if (vendas == null || !vendas.Any())
                return ResultHelper.NotFound<List<VendaResponseDTO>>(Mensagens.NenhumRegistro);

            var dtos = vendas.Select(v => new VendaResponseDTO(v)).ToList();
            return ResultHelper.Success(dtos);
        }

        public async Task<Result<VendaResponseDTO>> GetByIdAsync(int id)
        {
            var venda = await _uow.Repository<Venda>().FirstOrDefaultAsync(new VendasSpec(x => x.VendaId == id));

            if (venda == null)
                return ResultHelper.NotFound<VendaResponseDTO>(Mensagens.NenhumRegistro);

            return ResultHelper.Success(new VendaResponseDTO(venda));
        }


        public async Task<Result<VendaResponseDTO>> AtualizarStatusPagamentoAsync(VendaRequestDTO input)
        {
            var venda = await _uow.Repository<Venda>().FirstOrDefaultAsync(v => v.VendaId == input.VendaId);

            if (venda == null)
                return ResultHelper.NotFound<VendaResponseDTO>(Mensagens.NenhumRegistro);

            var sucesso = _uow.UseTransaction(() =>
            {
                venda.StatusPagamento = input.NovoStatus;
                _uow.Repository<Venda>().Update(venda);
                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<VendaResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new VendaResponseDTO(venda), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<VendaResponseDTO>> RealizarVendaAsync(VendaRequestDTO input)
        {
            var apartamento = await _uow.Repository<Apartamento>().FirstOrDefaultAsync(x => x.CodigoApartamento == input.CodigoApartamento);
            if (apartamento == null)
                return ResultHelper.NotFound<VendaResponseDTO>("Apartamento não encontrado.");

            if (apartamento.Status == DisponibilidadeStatus.Vendido || apartamento.Status == DisponibilidadeStatus.Reservado)
                return ResultHelper.Invalid<VendaResponseDTO>("Apartamento indisponível para venda.");

            var reservasAtivas = await _uow.Repository<Reserva>().FindAsync(x =>
                x.ApartamentoId == apartamento.ApartamentoId &&
                x.Status != StatusReservaEnum.Cancelada &&
                x.Status != StatusReservaEnum.Expirada &&
                x.Status != StatusReservaEnum.ConvertidaVenda);

            if (reservasAtivas.Any())
                return ResultHelper.Invalid<VendaResponseDTO>("O apartamento possui reservas ativas.");

            var venda = input.GetModel();

            var sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Venda>().Add(venda);

                apartamento.Status = DisponibilidadeStatus.Vendido;
                apartamento.DataUltimaAtualizacaoStatus = DateTime.UtcNow;
                _uow.Repository<Apartamento>().Update(apartamento);

                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<VendaResponseDTO>(Mensagens.ErroTransacao);

            return ResultHelper.Success(new VendaResponseDTO(venda), Mensagens.SucessoAoSalvar);
        }

        public async Task<Result<ReservaResponseDTO>> RealizarReservaAsync(VendaRequestDTO input)
        {
            // Buscar apartamento com includes
            var apartamento = await _uow.Repository<Apartamento>()
                .FirstOrDefaultAsync(new ApartamentoSpec(a => a.CodigoApartamento == input.CodigoApartamento));

            if (apartamento == null)
                return ResultHelper.NotFound<ReservaResponseDTO>("Apartamento não encontrado.");

            if (apartamento.Status != DisponibilidadeStatus.Disponivel)
                return ResultHelper.Invalid<ReservaResponseDTO>("O apartamento não está disponível para reserva.");

            // Verificar se já existe uma reserva ativa
            bool existeReservaAtiva = apartamento.Reservas.Any(r =>
                r.Status != StatusReservaEnum.Cancelada &&
                r.Status != StatusReservaEnum.Expirada &&
                r.Status != StatusReservaEnum.ConvertidaVenda);

            if (existeReservaAtiva)
                return ResultHelper.Invalid<ReservaResponseDTO>("Já existe uma reserva ativa para este apartamento.");

            var cliente = _uow.Repository<Cliente>().FirstOrDefaultAsync(x => x.CPF == input.ClienteCPF);

            var reserva = input.GetReserva();

            if (cliente != null)
                reserva.ClienteId = cliente.Result.ClienteId;

            bool sucesso = _uow.UseTransaction(() =>
            {
                _uow.Repository<Reserva>().Add(reserva);
                apartamento.Status = DisponibilidadeStatus.Reservado;
                apartamento.DataUltimaAtualizacaoStatus = DateTime.UtcNow;
                _uow.Repository<Apartamento>().Update(apartamento);
                _uow.Complete();
            });

            var dto = new ReservaResponseDTO(reserva);

            dto.NomeCliente = cliente.Result.ClienteNome;

            if (!sucesso)
                return ResultHelper.Invalid<ReservaResponseDTO>("Erro ao realizar a reserva.");

            return ResultHelper.Success(dto, "Reserva realizada com sucesso.");
        }

        public async Task<Result<string>> CancelarReservaAsync(string codigoApartamento)
        {
            var apartamento = await _uow.Repository<Apartamento>()
                .FirstOrDefaultAsync(new ApartamentoSpec(a => a.CodigoApartamento == codigoApartamento));

            if (apartamento == null)
                return ResultHelper.NotFound<string>("Apartamento não encontrado.");

            var reserva = await _uow.Repository<Reserva>()
                .FirstOrDefaultAsync(new ReservaSpec(r =>
                    r.ApartamentoId == apartamento.ApartamentoId &&
                    r.Status != StatusReservaEnum.Cancelada &&
                    r.Status != StatusReservaEnum.Expirada &&
                    r.Status != StatusReservaEnum.ConvertidaVenda
                ));

            if (reserva == null)
                return ResultHelper.NotFound<string>("Nenhuma reserva ativa encontrada para este apartamento.");

            var sucesso = _uow.UseTransaction(() =>
            {
                reserva.Status = StatusReservaEnum.Cancelada;
                _uow.Repository<Reserva>().Update(reserva);

                apartamento.Status = DisponibilidadeStatus.Disponivel;
                apartamento.DataUltimaAtualizacaoStatus = DateTime.UtcNow;
                _uow.Repository<Apartamento>().Update(apartamento);

                _uow.Complete();
            });

            if (!sucesso)
                return ResultHelper.Invalid<string>("Erro ao cancelar a reserva.");

            return ResultHelper.Success("Reserva cancelada com sucesso.");
        }

    }
}
