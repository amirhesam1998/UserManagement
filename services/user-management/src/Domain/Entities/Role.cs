using Domain.ValueObject;
using Shared.ResultManagement;


namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        private readonly List<Permission> _permissions = new();
        public IReadOnlyCollection<Permission> Permissions => _permissions;

        public ICollection<User> Users { get; private set; } = new List<User>();

        protected Role() { }

        public Role(string title , string description)
        {
            
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Rule Title is required.") : title.Trim();
            Description = description;
        }

        

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
