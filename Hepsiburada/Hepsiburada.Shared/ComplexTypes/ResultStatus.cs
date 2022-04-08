namespace Hepsiburada.Shared.ComplexTypes
{
    public enum ResultStatus : byte
    {
        Success = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        DataNull = 4,
        Authority = 5,
        IsExists = 6,
        ServiceError = 7
    }
}
