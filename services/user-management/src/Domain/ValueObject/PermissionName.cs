using Shared.ResultManagement;


namespace Domain.ValueObject
{
    public sealed class PermissionName : IEquatable<PermissionName>
    {
        public string Value { get; private set; }

        private PermissionName() { }

        private PermissionName(string value)
        {
            Value = value;
        }

        public static Result<PermissionName, string> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<PermissionName, string>.Failure("Permission name cannot be empty");

            return Result<PermissionName, string>.Success(new PermissionName(value.Trim()));
        }
        public override string ToString() => Value;

        public override bool Equals(object? obj) => Equals(obj as PermissionName);

        public bool Equals(PermissionName? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

        public static bool operator ==(PermissionName? left, PermissionName? right) => Equals(left, right);
        public static bool operator !=(PermissionName? left, PermissionName? right) => !Equals(left, right);
    }
}
