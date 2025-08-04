using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enum;

namespace ImoveisConnect.Domain.DTOS.Response
{
    public class VendaResponseDTO
    {
        public int VendaId { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public int ApartamentoId { get; set; }
        public string NumeroApartamento { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorEntrada { get; set; }
        public string Observacoes { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }

        public VendaResponseDTO(Venda venda)
        {
            if (venda == null) return;

            VendaId = venda.VendaId;
            ClienteId = venda.ClienteId;
            NomeCliente = venda.Cliente?.ClienteNome;
            ApartamentoId = venda.ApartamentoId;
            NumeroApartamento = venda.Apartamento?.CodigoApartamento;
            DataVenda = venda.DataVenda;
            ValorEntrada = venda.ValorEntrada;
            Observacoes = venda.Observacoes;
            StatusPagamento = venda.StatusPagamento;
        }
    }
}
