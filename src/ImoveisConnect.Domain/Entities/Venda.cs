using ImoveisConnect.Application.Core;
using ImoveisConnect.Domain.Enum;

namespace ImoveisConnect.Domain.Entities
{
    public class Venda : BaseEntity
    {
        public int VendaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ApartamentoId { get; set; }
        public Apartamento Apartamento { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorEntrada { get; set; }
        public string Observacoes { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}
