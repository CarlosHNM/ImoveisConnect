using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enum;

namespace ImoveisConnect.Domain.DTOS.Response
{
    public class ReservaResponseDTO
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } // opcional, se Cliente for incluído
        public int ApartamentoId { get; set; }
        public string CodigoApartamento { get; set; } // opcional, se Apartamento for incluído
        public DateTime DataReserva { get; set; }
        public DateTime DataExpiracao { get; set; }
        public StatusReservaEnum Status { get; set; }
        public string ValorReserva { get; set; }
        public string Observacoes { get; set; }

        public ReservaResponseDTO() { }

        public ReservaResponseDTO(Reserva reserva)
        {
            ReservaId = reserva.ReservaId;
            ClienteId = reserva.ClienteId;
            NomeCliente = reserva.Cliente?.ClienteNome; // garantir include
            ApartamentoId = reserva.ApartamentoId;
            CodigoApartamento = reserva.Apartamento?.CodigoApartamento; // garantir include
            DataReserva = reserva.DataReserva;
            DataExpiracao = reserva.DataExpiracao;
            Status = reserva.Status;
            ValorReserva = reserva.ValorReserva;
            Observacoes = reserva.Observacoes;
        }
    }
}
