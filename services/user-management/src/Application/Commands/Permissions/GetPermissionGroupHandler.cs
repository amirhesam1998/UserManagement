
using System.Data;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionGroupHandler : IRequestHandler<GetPermissionGroupCommand , Result<List<Permission>? , string>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetPermissionGroupHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<List<Permission>?, string>> Handle(GetPermissionGroupCommand request, CancellationToken cancellationToken)
        {
            //var permission = await _permissionRepository.GetPermissionGroupAsync(request.Id);

            //if (permission == null)
            //{
            //    return Result<List<Permission>?, string>.Failure("Permission not found.");
            //}

            //if (permission.PermissionId != null)
            //{
            //    return Result<List<Permission>?, string>.Failure("This permission is not a group head.");
            //}

            //// گرفتن خودش و زیرمجموعه‌ها
            //var allPermissions = await _permissionRepository.GetAllPermissionAsync();
            //var groupPermissions = allPermissions
            //    ?.Where(p => p.Id == request.Id || p.PermissionId == request.Id)
            //    .ToList();

            //return Result<List<Permission>?, string>.Success(groupPermissions);

            var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (permission == null)
            {
                return Result<List<Permission>?, string>.Failure("Permission not found.");
            }

            if (permission.PermissionId != null)
            {
                return Result<List<Permission>?, string>.Failure("This permission is not a group head.");
            }

            // گرفتن خودش + زیرمجموعه‌ها
            var groupPermissions = await _permissionRepository.GetPermissionGroupAsync(permission);

            return Result<List<Permission>?, string>.Success(groupPermissions);
        }
    }
}
