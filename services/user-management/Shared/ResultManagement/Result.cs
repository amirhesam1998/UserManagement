
namespace Shared.ResultManagement
{
    

    public sealed class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }

        private Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);

        public static Result Failure(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("Error message cannot be empty.", nameof(error));

            return new Result(false, error);
        }

        /// <summary>
        /// تطبیق نتیجه برای اجرای عملیات بر اساس موفقیت یا خطا
        /// </summary>
        public T Match<T>(Func<T> onSuccess, Func<string, T> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(Error!);
        }

        /// <summary>
        /// تبدیل ضمنی به موفقیت/خطا با string
        /// </summary>
        public static implicit operator Result(string error) => Failure(error);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        public bool IsFailure => !IsSuccess;
        public T Value { get; }

        protected Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null!;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
            Value = default!;
        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(Error error) => new(error);
    }

    public sealed class Result<TSuccess, TFailure>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public TSuccess? Value { get; }
        public TFailure? Error { get; }

        private Result(bool isSuccess, TSuccess? value, TFailure? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<TSuccess, TFailure> Success(TSuccess value)
        {
            return new Result<TSuccess, TFailure>(true, value, default);
        }

        public static Result<TSuccess, TFailure> Failure(TFailure error)
        {
            return new Result<TSuccess, TFailure>(false, default, error);
        }

        /// <summary>
        /// تطبیق موفق/ناموفق برای تصمیم‌گیری مستقیم
        /// </summary>
        public TResult Match<TResult>(
            Func<TSuccess, TResult> onSuccess,
            Func<TFailure, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }

        /// <summary>
        /// نگاشت نتیجه موفق به یک مقدار جدید
        /// </summary>
        public Result<TNewSuccess, TFailure> Map<TNewSuccess>(Func<TSuccess, TNewSuccess> map)
        {
            return IsSuccess
                ? Result<TNewSuccess, TFailure>.Success(map(Value!))
                : Result<TNewSuccess, TFailure>.Failure(Error!);
        }

        /// <summary>
        /// زنجیره‌سازی توابع موفق
        /// </summary>
        public Result<TNewSuccess, TFailure> Bind<TNewSuccess>(
            Func<TSuccess, Result<TNewSuccess, TFailure>> bind)
        {
            return IsSuccess ? bind(Value!) : Result<TNewSuccess, TFailure>.Failure(Error!);
        }

        /// <summary>
        /// تبدیل ضمنی از موفقیت
        /// </summary>
        public static implicit operator Result<TSuccess, TFailure>(TSuccess value)
            => Success(value);

        /// <summary>
        /// تبدیل ضمنی از شکست
        /// </summary>
        public static implicit operator Result<TSuccess, TFailure>(TFailure error)
            => Failure(error);
    }

    //public class Result<TSuccess, TFailure>
    //{
    //    public bool IsSuccess { get; }
    //    public bool IsFailure => !IsSuccess;

    //    public TSuccess? Value { get; }
    //    public TFailure? Error { get; }

    //    protected Result(TSuccess value)
    //    {
    //        IsSuccess = true;
    //        Value = value;
    //        Error = default;
    //    }

    //    protected Result(TFailure error)
    //    {
    //        IsSuccess = false;
    //        Error = error;
    //        Value = default;
    //    }

    //    public static Result<TSuccess, TFailure> Success(TSuccess value) => new(value);
    //    public static Result<TSuccess, TFailure> Failure(TFailure error) => new(error);
    //}
}
