
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AddPermissionHandler : IRequestHandler<AddPermissionCommand , Result<Guid,string>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public AddPermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<Guid, string>> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionName = PermissionName.Create(request.Name);
            if (!permissionName.IsSuccess)
                return Result<Guid, string>.Failure(permissionName.Error!);
            var permission = Permission.Create(
                permissionName.Value,
                request.Ordering,
                request.Active,
                request.Description
            );
            if (!permission.IsSuccess)
                return Result<Guid, string>.Failure(permission.Error!);
            if (!string.IsNullOrWhiteSpace(request.PermissionId))
            {
                if (Guid.TryParse(request.PermissionId, out var parsedId))
                {
                    permission.Value.SetPermissionId(parsedId);
                }
                else
                {
                    return Result<Guid, string>.Failure("PermissionId is not a valid GUID.");
                }
            }
            await _permissionRepository.AddAsync(permission.Value, cancellationToken);
            return Result<Guid, string>.Success(permission.Value.Id);
        }

    }
}
