using ImoveisConnect.Domain.Entities;

namespace ImoveisConnect.Domain.DTOS.Response
{
    public class ClienteResponseDTO
    {
        public ClienteResponseDTO(Cliente input)
        {
            ClienteId = input.ClienteId;
            ClienteNome = input.ClienteNome;
            ClienteEmail = input.ClienteEmail;
            Telefone = input.Telefone;
            CPF = input.CPF;
            DataCadastro = input.DataCadastro;
            Vendas = input.Vendas;
            Reservas = input.Reservas;
        }

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
