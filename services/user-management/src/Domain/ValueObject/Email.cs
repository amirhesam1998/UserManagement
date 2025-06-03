using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Shared.ResultManagement;
namespace Domain.ValueObject
{
    public sealed class Email : IEquatable<Email>
    {
        private static readonly Regex EmailRegex = new(@"^[\w\.\-]+@([\w\-]+\.)+[a-zA-Z]{2,}$");
        public string Value { get; private set; }

        private Email() { }

        [JsonConstructor]
        private Email(string value)
        {
            Value = value;
        }

        
        public static Result<Email, string> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result<Email, string>.Failure("Email cannot be empty");
            if (!EmailRegex.IsMatch(value))
                return Result<Email, string>.Failure("Invalid email format.");
            return Result<Email, string>.Success(new Email(value));
        }


        public override string ToString() => Value;
        public override bool Equals(object? obj) => Equals(obj as Email);
        public bool Equals(Email? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }
        public override int GetHashCode() => Value.GetHashCode();
        public static bool operator ==(Email? left, Email? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Email? left, Email? right)
        {
            return !Equals(left, right);
        }


        
    }
}
