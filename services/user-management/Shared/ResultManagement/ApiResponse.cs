
namespace Shared.ResultManagement
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        private ApiResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public static ApiResponse Ok(string? message = null) => new(true, message);
        public static ApiResponse Fail(string message) => new(false, message);
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        private ApiResponse(bool success, T? data = default, string? error = null)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public static ApiResponse<T> Ok(T data) => new(true, data);
        public static ApiResponse<T> Fail(string error) => new(false, default, error);
    }
}
