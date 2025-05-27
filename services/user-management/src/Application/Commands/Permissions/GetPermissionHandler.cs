
using Application.Commands.Roles;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionHandler : IRequestHandler<GetPermissionCommand,Result<Permission,string>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetPermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<Permission, string>> Handle(GetPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id , cancellationToken);
            if (permission is null)
                return Result<Permission, string>.Failure("permission not found");

            return Result<Permission, string>.Success(permission);
        }
    }
}
