
using Application.Interfaces;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AssignPermissionsToRoleHandler : IRequestHandler<AssignPermissionsToRoleCommand, Result<bool, string>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public AssignPermissionsToRoleHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<bool, string>> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleAsync(request.RoleId, cancellationToken);
            if (role == null)
                return Result<bool, string>.Failure("Role not found.");

            var permissions = await _permissionRepository.GetByIdsAsync(request.PermissionIds, cancellationToken);
            if (permissions.Count != request.PermissionIds.Count)
                return Result<bool, string>.Failure("One or more permission IDs are invalid.");

            foreach (var permission in permissions)
            {
                role.AddPermission(permission);
            }

            await _roleRepository.UpdateAsync(role, cancellationToken);

            return Result<bool, string>.Success(true);
        }
    }
}
