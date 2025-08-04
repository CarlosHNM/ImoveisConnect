using ImoveisConnect.Domain.Enum;

namespace ImoveisConnect.Domain.DTOS.Request
{
    public class VendaRequestDTO
    {
        public string ClienteCPF { get; set; }
        public string CodigoApartamento { get; set; }
        public string ValorReserva { get; set; }
        public string Observacoes { get; set; }

        public int VendaId { get; set; }
        public StatusPagamentoEnum NovoStatus { get; set; }

        public decimal ValorEntrada { get; set; }

        public Domain.Entities.Venda GetModel()
        {
            return new Domain.Entities.Venda
            {
                DataVenda = DateTime.Now,
                ValorEntrada = ValorEntrada,
                Observacoes = Observacoes,
                StatusPagamento = NovoStatus
            };
        }

        public Domain.Entities.Reserva GetReserva()
        {
            return new Domain.Entities.Reserva
            {
                DataReserva = DateTime.Now,
                DataExpiracao = DateTime.Now.AddDays(3), // exemplo: reserva válida por 3 dias
                Status = StatusReservaEnum.Ativa,
                ValorReserva = ValorReserva,
                Observacoes = Observacoes
            };
        }
    }
}
