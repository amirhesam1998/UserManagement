
namespace Shared.ResultManagement
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code}: {Message}";
    }
    public static class Errors
    {
        public static class General
        {
            public static readonly Error Unexpected = new("General.Unexpected", "An unexpected error occurred.");
        }

        public static class Validation
        {
            public static Error Invalid(string field, string reason) =>
                new($"Validation.{field}", reason);
        }

        public static class User
        {
            public static readonly Error NotFound = new("User.NotFound", "User not found.");
            public static readonly Error DuplicateEmail = new("User.DuplicateEmail", "Email already exists.");
        }

        public static class Auth
        {
            public static readonly Error InvalidCredentials = new("Auth.InvalidCredentials", "Invalid username or password.");
        }
    }
}
