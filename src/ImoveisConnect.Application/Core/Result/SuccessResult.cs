using ImoveisConnect.Application.Core.Result.Enums;

namespace ImoveisConnect.Application.Core.Result
{
    public class SuccessResult<T> : Result<T>
    {
        private readonly T _data;
        private readonly string _success;

        public SuccessResult(T data, string success)
        {
            _data = data;
            _success = success ?? Mensagens.SucessoDefault ?? "Operação realizada com sucesso";
        }

        public override ResultType Type => ResultType.Success;

        public override List<string> Errors => new List<string>();

        public override T Data => _data;

        public override int TotalRecords => 0;

        public override string Success => _success;
    }
}
