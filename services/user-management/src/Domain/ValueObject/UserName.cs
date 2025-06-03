using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Shared.ResultManagement;

namespace Domain.ValueObject
{
    public sealed class UserName : IEquatable<UserName>
    {
        private static readonly Regex PhoneRegex = new(@"^\+?[0-9]{10,15}$");
        public string Value { get; private set; }
        private UserName() { }
        [JsonConstructor]
        private UserName(string value)
        {
            Value = value;
        }
        public static Result<UserName, string> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result<UserName, string>.Failure("PhoneNumber cannot be empty");
            if (!PhoneRegex.IsMatch(value) || value.Length != 11)
                return Result<UserName, string>.Failure("Invalid PhoneNumber");
            return Result<UserName, string>.Success(new UserName(value));
        }
        public override string ToString() => Value;
        public override bool Equals(object? obj) => Equals(obj as UserName);
        public bool Equals(UserName? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }
        public override int GetHashCode() => Value.GetHashCode();
        public static bool operator ==(UserName? left, UserName? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserName? left, UserName? right)
        {
            return !Equals(left, right);
        }

    }
}
