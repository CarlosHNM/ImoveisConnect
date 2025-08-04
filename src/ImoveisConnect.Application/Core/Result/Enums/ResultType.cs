using System.ComponentModel;

namespace ImoveisConnect.Application.Core.Result.Enums
{
    /// <summary>
    /// Enumerador que tipa a resposta padrão das requisições dos services
    /// </summary>
    public enum ResultType
    {
        [Description("Success")]
        /// <summary>
        /// Realizada com sucesso.
        /// </summary>
        Success,

        [Description("Invalid")]
        /// <summary>
        /// Ocorreu que algum dado de entrada ou regra não foi satisfeita.
        /// </summary>
        Invalid,

        [Description("NotFound")]
        /// <summary>
        /// Não foi possível carregar a(s) informação(ções).
        /// </summary>
        NotFound,
        /// <summary>
        /// Página de uma consulta com a lista das informações e total de registros para a condição de pesquisa.
        /// </summary>
        [Description("Page")]
        Page,
    }
}
