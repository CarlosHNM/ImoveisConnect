using System.ComponentModel;

namespace ImoveisConnect.Domain.Enum
{
    public enum StatusPagamentoEnum
    {
        Nullo = 0,
        [Description("Aguardando Pagamento")]
        Aguardando = 1,

        [Description("Pagamento Aprovado")]
        Aprovado = 2,

        [Description("Pagamento Não Processado")]
        NaoProcessado = 3,
    }
}
