using ImoveisConnect.Application.Core.Result.Enums;

namespace ImoveisConnect.Application.Core.Result
{
    /// <summary>
    /// Resposta inválida da padrão
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvalidResult<T> : Result<T>
    {
        private readonly List<string> _errors;

        public InvalidResult(string error)
        {
            _errors = new List<string> { error ?? Mensagens.InvalidoDefault };
        }

        public InvalidResult(List<string> errors)
        {
            _errors = errors ?? new List<string> { Mensagens.InvalidoDefault };
        }

        public override ResultType Type => ResultType.Invalid;

        public override List<string> Errors => _errors;

        public override T Data => default;

        public override int TotalRecords => 0;

        public override string Success => null;
    }
}
