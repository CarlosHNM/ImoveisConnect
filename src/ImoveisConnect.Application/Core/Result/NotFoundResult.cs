using ImoveisConnect.Application.Core.Result.Enums;

namespace ImoveisConnect.Application.Core.Result
{
    /// <summary>
    /// Resposta dados não encontrados padrão
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotFoundResult<T> : Result<T>
    {
        private readonly List<string> _errors;

        public NotFoundResult(string error)
        {
            _errors = new List<string> { error ?? Mensagens.InvalidoDefault };
        }

        public NotFoundResult(List<string> errors)
        {
            _errors = errors ?? new List<string> { Mensagens.InvalidoDefault };
        }

        public override ResultType Type => ResultType.NotFound;

        public override List<string> Errors => _errors;

        public override T Data => default;

        public override int TotalRecords => 0;

        public override string Success => null;
    }
}
