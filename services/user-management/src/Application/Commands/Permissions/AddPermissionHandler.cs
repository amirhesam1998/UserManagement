
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AddPermissionHandler : IRequestHandler<AddPermissionCommand , Result<int,string>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public AddPermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<int, string>> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionName = PermissionName.Create(request.Name);
            if (!permissionName.IsSuccess)
                return Result<int, string>.Failure(permissionName.Error!);
            var permission = Permission.Create(
                permissionName.Value,
                request.Active,
                request.Description
            );
            if (!permission.IsSuccess)
                return Result<int, string>.Failure(permission.Error!);
            if (!string.IsNullOrWhiteSpace(request.PermissionId))
            {
                if (int.TryParse(request.PermissionId, out var parsedId))
                {
                    permission.Value.SetPermissionId(parsedId);
                }
                else
                {
                    return Result<int,string>.Failure("PermissionId is not a valid GUID.");
                }
            }
            await _permissionRepository.AddAsync(permission.Value, cancellationToken);
            return Result<int, string>.Success(permission.Value.Id);
        }

    }
}
