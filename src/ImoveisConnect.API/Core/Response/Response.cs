using ImoveisConnect.Application;
using ImoveisConnect.Application.Core.Result.Enums;
using ImoveisConnect.Application.Extensions;

namespace ImoveisConnect.API.Core.Response
{
    public class Response<T>
    {
        private ResultType? _resultType { get; set; }

        protected Response()
        {
        }

        public Response(T data, string success = null, ResultType? resultType = null)
        {
            Success = success ?? Mensagens.SucessoDefault;
            Errors = null;
            Data = data;
            Failures = null;
            _resultType = resultType;
        }

        public Response(string error, ResultType? resultType = null)
        {
            Errors = new List<string> { error ?? Mensagens.ErroDefault };
            Data = default;
            Failures = null;
            _resultType = resultType;
        }

        public Response(List<string> errors, ResultType? resultType = null)
        {
            Errors = errors ?? new List<string> { Mensagens.ErroDefault };
            _resultType = resultType;
        }

        public Response(IDictionary<string, IEnumerable<string>> failures, ResultType? resultType = null)
        {
            Errors = null;
            Data = default;
            Failures = failures;
            _resultType = resultType;
        }

        public string ResultType
        {
            get
            {
                if (_resultType.HasValue)
                    return _resultType.GetDescription();

                return null;
            }
        }
        public T Data { get; protected set; }
        public string Success { get; protected set; }
        public List<string> Errors { get; protected set; }
        public IDictionary<string, IEnumerable<string>> Failures { get; protected set; }
    }
}
