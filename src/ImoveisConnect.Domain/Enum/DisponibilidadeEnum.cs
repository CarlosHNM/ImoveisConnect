using System.ComponentModel;

namespace ImoveisConnect.Domain.Enums
{
    public enum DisponibilidadeStatus
    {
        [Description("Disponível para Venda")]
        Disponivel = 1,

        [Description("Reservado - Aguardando Confirmação")]
        Reservado = 2,

        [Description("Vendido - Negócio Concluído")]
        Vendido = 3,

        [Description("Indisponível - Em Manutenção")]
        EmManutencao = 4,

        [Description("Bloqueado - Pendente Documentação")]
        Bloqueado = 5,

        [Description("Reserva Expirada")]
        ReservaExpirada = 6,

        [Description("Venda em Processamento")]
        VendaProcessamento = 7
    }
}