
using Domain.Enums;
using Domain.ValueObject;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }
        public UserName Username { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public UserType Type { get; private set; } = UserType.User;

        private readonly List<Role> _roles = new();
        public IReadOnlyCollection<Role> Roles => _roles;
        protected User() { }

        public User(string firstName, string lastName, Email email, string passwordHash, UserName username , UserType type = UserType.User)
        {
            Id = Guid.NewGuid();
            FirstName = string.IsNullOrWhiteSpace(firstName) ? throw new ArgumentException("First name is required.") : firstName.Trim();
            LastName = string.IsNullOrWhiteSpace(lastName) ? throw new ArgumentException("Last name is required.") : lastName.Trim();
            Email = email;
            Password = string.IsNullOrWhiteSpace(passwordHash) ? throw new ArgumentException("Password hash is required.") : passwordHash;
            Username = username;
            CreatedAt = DateTime.UtcNow;
            Type = type;
        }


        public void AssignRole(Role role)
        {
            if (!_roles.Any(r => r.Title == role.Title))
                _roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            _roles.RemoveAll(r => r.Title == role.Title);
        }

        public bool HasPermission(PermissionName permission)
        {
            return _roles
                .SelectMany(r => r.Permissions)
                .Any(p => p.Name == permission);
        }

    }
}
