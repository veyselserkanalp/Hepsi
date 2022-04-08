using Hepsiburada.Shared.ComplexTypes;
using Hepsiburada.Shared.Results.Abstract;

namespace Hepsiburada.Shared.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult()
        {

        }
        public DataResult(ResultStatus resultStatus, bool success)
        {
            ResultStatus = resultStatus;
            Success = success;

        }
        public DataResult(T data, bool success)
        {
            Data = data;
            Success = success;
        }
        public DataResult(ResultStatus resultStatus, T data, bool success)
        {
            ResultStatus = resultStatus;
            Data = data;
            Success = success;

        }
        public DataResult(ResultStatus resultStatus, string message, bool success)
        {
            ResultStatus = resultStatus;
            Message = message;
            Success = success;

        }
        public DataResult(ResultStatus resultStatus, string message, T data, bool success)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Success = success;

        }
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }


    }
}