using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Venda> Vendas { get; set; } = new List<Venda>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
