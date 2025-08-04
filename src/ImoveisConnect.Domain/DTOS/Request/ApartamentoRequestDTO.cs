using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enums;

namespace ImoveisConnect.Domain.DTOS.Request
{
    public class ApartamentoRequestDTO
    {
        public string CodigoApartamento { get; set; }
        public string Endereco { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int NumeroQuartos { get; set; }
        public decimal Area { get; set; }

        public Apartamento GetModel()
        {
            return new Apartamento
            {
                CodigoApartamento = CodigoApartamento,
                Endereco = Endereco,
                Preco = Preco,
                Descricao = Descricao,
                NumeroQuartos = NumeroQuartos,
                Area = Area,
                Status = DisponibilidadeStatus.Disponivel,
                DataCadastro = DateTime.UtcNow,
                DataUltimaAtualizacaoStatus = DateTime.UtcNow
            };
        }
    }
}
