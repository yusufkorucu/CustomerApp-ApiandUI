namespace CustomerApp.Api.Infrastructure.Utilities
{
    public class CustomerApiResponse<T> : ICustomerApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }

        public CustomerApiResponse() { }

        public CustomerApiResponse(bool isSuccess, string message, Exception exception, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = exception;
            Data = data;
        }

        public CustomerApiResponse(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public CustomerApiResponse(bool isSuccess, T data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public CustomerApiResponse(bool isSuccess, string message, Exception exception)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = exception;
        }

        public CustomerApiResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public CustomerApiResponse(bool isSuccess, List<string> messages)
        {
            IsSuccess = isSuccess;
            Message = string.Join(",", messages);
        }

        public CustomerApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;

        }
    }
}
