using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enums;

namespace ImoveisConnect.Domain.DTOS.Response
{
    public class ApartamentoResponseDTO
    {
        public int ApartamentoId { get; set; }
        public string CodigoApartamento { get; set; }
        public string Endereco { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int NumeroQuartos { get; set; }
        public decimal Area { get; set; }
        public DisponibilidadeStatus Status { get; set; }
        public DateTime? DataUltimaAtualizacaoStatus { get; set; }
        public string MotivoIndisponibilidade { get; set; }

        public ApartamentoResponseDTO(Apartamento model)
        {
            ApartamentoId = model.ApartamentoId;
            CodigoApartamento = model.CodigoApartamento;
            Endereco = model.Endereco;
            Preco = model.Preco;
            Descricao = model.Descricao;
            NumeroQuartos = model.NumeroQuartos;
            Area = model.Area;
            Status = model.Status;
            DataUltimaAtualizacaoStatus = model.DataUltimaAtualizacaoStatus;
            MotivoIndisponibilidade = model.MotivoIndisponibilidade;
        }
    }
}
