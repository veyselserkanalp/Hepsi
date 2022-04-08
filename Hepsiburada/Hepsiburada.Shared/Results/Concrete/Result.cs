using Hepsiburada.Shared.ComplexTypes;
using Hepsiburada.Shared.Results.Abstract;
using System;

namespace Hepsiburada.Shared.Results.Concrete
{
    public class Result : IResult
    {
        public Result() { }
        public Result(bool success)
        {
            Success = success;
        }
        public Result(ResultStatus resultStatus, bool success)
        {
            ResultStatus = resultStatus;
            Success = success;
        }
        public Result(ResultStatus resultStatus, string message, bool success)
        {
            ResultStatus = resultStatus;
            Message = message;
            Success = success;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception, bool success)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception.Message;
            Success = success;
        }
        public ResultStatus ResultStatus { get; }
        public bool Success { get; set; }
        public string Message { get; }
        public string Exception { get; }
    }
}