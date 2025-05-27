
using Domain.ValueObject;
using Shared.ResultManagement;

namespace Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; private set; }
        
        public PermissionName Name { get; private set; }
        public Guid? PermissionId { get; private set; }
        public int Ordering {  get; private set; }
        public bool Active { get; private set; }
        public string? Description { get; private set; }


        private readonly List<Role> _roles = new();
        public IReadOnlyCollection<Role> Roles => _roles;

        protected Permission() { }
        public Permission(PermissionName permission , int ordering , bool active , string description)
        {
            Id = Guid.NewGuid();
            PermissionId = null; 
            Name = permission;
            Ordering = ordering;
            Active = active;
            Description = description;
        }

        public static Result<Permission, string> Create(PermissionName name , int ordering , bool active , string description)
        {
            if (name == null)
                return Result<Permission, string>.Failure("Permission name is required.");

            return Result<Permission, string>.Success(new Permission(name , ordering , active , description));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Permission other) return false;
            return Name == other.Name;
        }
        public void UpdateOrdering(int newOrdering) => Ordering = newOrdering;
        public void ToggleActive() => Active = !Active;
        public void SetPermissionId(Guid? permissionId)
        {
            PermissionId = permissionId;
        }
        public override int GetHashCode() => Name.GetHashCode();

    }
}
