
using Domain.ValueObject;
using Shared.ResultManagement;

namespace Domain.Entities
{
    public class Permission
    {
        public int Id { get; private set; }
        
        public PermissionName Name { get; private set; }
        public int? PermissionId { get; private set; }
        public bool Active { get; private set; }
        public string? Description { get; private set; }


        private readonly List<Role> _roles = new();
        public IReadOnlyCollection<Role> Roles => _roles;

        protected Permission() { }
        public Permission(PermissionName permission, bool active , string description)
        {
            PermissionId = null; 
            Name = permission;
            Active = active;
            Description = description;
        }

        public static Result<Permission, string> Create(PermissionName name , bool active , string description)
        {
            if (name == null)
                return Result<Permission, string>.Failure("Permission name is required.");

            return Result<Permission, string>.Success(new Permission(name , active , description));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Permission other) return false;
            return Name == other.Name;
        }
        public void ToggleActive() => Active = !Active;
        public void SetPermissionId(int? permissionId)
        {
            PermissionId = permissionId;
        }
        public override int GetHashCode() => Name.GetHashCode();

    }
}
