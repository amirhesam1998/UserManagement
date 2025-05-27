
using System.Text.RegularExpressions;

namespace Shared.ResultManagement
{
    public static class Guard
    {
        public static Result<string> AgainstNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<string>.Failure(Errors.Validation.Invalid(fieldName, $"{fieldName} cannot be empty"));
            return Result<string>.Success(value);
        }

        public static Result<string> AgainstLength(string value, string fieldName, int min, int max)
        {
            if (value.Length < min || value.Length > max)
                return Result<string>.Failure(Errors.Validation.Invalid(fieldName, $"{fieldName} must be between {min} and {max} characters"));
            return Result<string>.Success(value);
        }

        public static Result<string> AgainstRegex(string value, string fieldName, Regex regex, string errorMessage)
        {
            if (!regex.IsMatch(value))
                return Result<string>.Failure(Errors.Validation.Invalid(fieldName, errorMessage));
            return Result<string>.Success(value);
        }
    }
}
