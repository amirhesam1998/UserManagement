
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AssignPermissionsToRoleCommand : IRequest<Result<bool, string>>
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }

        public AssignPermissionsToRoleCommand(int roleId , List<int> permissionIds)
        {
            RoleId = roleId;
            PermissionIds = permissionIds;
        }
    }
}
