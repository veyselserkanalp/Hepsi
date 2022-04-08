using Hepsiburada.Shared.ComplexTypes;

namespace Hepsiburada.Shared.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        public T Data { get; }

    }
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public bool Success { get; }


    }
}