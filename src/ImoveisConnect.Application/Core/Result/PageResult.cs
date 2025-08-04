using ImoveisConnect.Application.Core.Result.Enums;

namespace ImoveisConnect.Application.Core.Result
{
    public class PageResult<T> : Result<T>
    {
        private readonly T _data;
        private readonly int _totalRecords;
        private readonly string _success;

        public PageResult(T data, int totalRecords, string success)
        {
            _data = data;
            _totalRecords = totalRecords;
            _success = success;
        }

        public override ResultType Type => ResultType.Page;

        public override List<string> Errors => new List<string>();

        public override T Data => _data;

        public override int TotalRecords => _totalRecords;

        public override string Success => _success;
    }
}
