using System.Text.RegularExpressions;
using Shared.ResultManagement;

namespace Domain.ValueObject
{
    public sealed class Slug : IEquatable<Slug>
    {
        private static readonly Regex SlugRegex = new("^[a-z0-9]+(?:-[a-z0-9]+)*$");

        public string Value { get; private set; }

        private Slug() { }

        private Slug(string value)
        {
            Value = value;
        }

        public static Result<Slug, string> Create(string value)
        {

            var normalized = Normalize(value);

            if (!SlugRegex.IsMatch(normalized))
                return Result<Slug, string>.Failure("Slug must contain only lowercase letters, digits, and hyphens.");

            return Result<Slug, string>.Success(new Slug(normalized));
        }

        private static string Normalize(string input)
        {
            var trimmed = input.Trim().ToLowerInvariant();
            var slug = Regex.Replace(trimmed, @"[^a-z0-9]+", "-");
            slug = Regex.Replace(slug, @"^-+|-+$", "");
            return slug;
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => Equals(obj as Slug);

        public bool Equals(Slug? other) => other is not null && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
