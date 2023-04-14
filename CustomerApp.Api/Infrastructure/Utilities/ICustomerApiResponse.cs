namespace CustomerApp.Api.Infrastructure.Utilities
{
    public interface ICustomerApiResponse
    {
        bool IsSuccess { get; }
        string Message { get; }
        Exception Exception { get; }
    }
}
