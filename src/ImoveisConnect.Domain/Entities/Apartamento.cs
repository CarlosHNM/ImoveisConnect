using ImoveisConnect.Application.Core;
using ImoveisConnect.Domain.Enums;

namespace ImoveisConnect.Domain.Entities
{
    public class Apartamento : BaseEntity
    {
        public int ApartamentoId { get; set; }
        public string CodigoApartamento { get; set; }
        public string Endereco { get; set; }
        public decimal Preco { get; set; }
        public DisponibilidadeStatus Status { get; set; } // Usando o enum
        public DateTime? DataUltimaAtualizacaoStatus { get; set; }
        public string? MotivoIndisponibilidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Descricao { get; set; }
        public int NumeroQuartos { get; set; }
        public decimal Area { get; set; }

        // Relacionamentos
        public List<Venda> Vendas { get; set; } = new List<Venda>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
