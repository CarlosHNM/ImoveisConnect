using System.ComponentModel;

namespace ImoveisConnect.Domain.Enum
{
    public enum StatusReservaEnum
    {
        [Description("Solicitada")]
        Solicitada = 1,

        [Description("Confirmada")]
        Confirmada = 2,

        [Description("Ativa")]
        Ativa = 3,

        [Description("Pendente Pagamento")]
        PendentePagamento = 4,

        [Description("Convertida em Venda")]
        ConvertidaVenda = 5,

        [Description("Cancelada")]
        Cancelada = 6,

        [Description("Expirada")]
        Expirada = 7,

        [Description("Em Análise")]
        EmAnalise = 8
    }
}
