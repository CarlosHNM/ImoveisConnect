using ImoveisConnect.Application.Core.Result.Enums;

namespace ImoveisConnect.Application.Core.Result
{
    public abstract class Result<T>
    {
        /// <summary>
        /// Tipo
        /// </summary>
        public abstract ResultType Type { get; }
        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public abstract List<string> Errors { get; }
        /// <summary>
        /// Mensagem de sucesso
        /// </summary>
        public abstract string Success { get; }
        /// <summary>
        /// Objeto a ser retornado
        /// </summary>
        public abstract T Data { get; }
        /// <summary>
        /// Total de registros
        /// </summary>
        public abstract int TotalRecords { get; }
    }
}
