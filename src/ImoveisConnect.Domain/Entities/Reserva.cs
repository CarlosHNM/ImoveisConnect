using ImoveisConnect.Application.Core;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enum;

namespace ImoveisConnect.Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ApartamentoId { get; set; }
        public DateTime DataReserva { get; set; }
        public DateTime DataExpiracao { get; set; }
        public StatusReservaEnum Status { get; set; }
        public string ValorReserva { get; set; }
        public string Observacoes { get; set; }

        public Apartamento Apartamento { get; set; }
    }
}
