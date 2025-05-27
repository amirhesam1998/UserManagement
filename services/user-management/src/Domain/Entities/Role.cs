using Domain.ValueObject;
using Shared.ResultManagement;


namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public Slug Slug { get; private set; }
        public string Description { get; private set; }

        private readonly List<Permission> _permissions = new();
        public IReadOnlyCollection<Permission> Permissions => _permissions;

        private readonly List<User> _users = new();
        public IReadOnlyCollection<User> Users => _users;
        protected Role() { }

        public Role(string title , Slug slug , string description)
        {
            Id = Guid.NewGuid();
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Rule Title is required.") : title.Trim();
            Slug = slug;
            Description = description;
        }

        //public static Result<Role, string> Create(RoleName name)
        //{
        //    if (name == null)
        //        return Result<Role, string>.Failure("Role name is required.");

        //    return Result<Role, string>.Success(new Role(name));
        //}

        public void AddPermission(Permission permission)
        {
            if (!_permissions.Any(p => p.Equals(permission)))
                _permissions.Add(permission);
        }

        public void RemovePermission(Permission permission)
        {
            _permissions.Remove(permission);
        }

    }
}
