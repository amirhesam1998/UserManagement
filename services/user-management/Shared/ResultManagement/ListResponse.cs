
namespace Shared.ResultManagement
{
    public class ListResponse<T>
    {
        public bool Success { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public string? Error { get; set; }

        private ListResponse(bool success, IEnumerable<T>? data = null, string? error = null)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public static ListResponse<T> Ok(IEnumerable<T> data) => new(true, data);
        public static ListResponse<T> Fail(string error) => new(false, null, error);
    }
}
