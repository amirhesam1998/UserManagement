
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetAllPermissionHandler : IRequestHandler<GetAllPermissionCommand , Result<List<Permission>,string>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetAllPermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<List<Permission>,string>> Handle(GetAllPermissionCommand request , CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetAllPermissionAsync();
            if (permission == null || permission.Count == 0)
            {
                return Result<List<Permission>, string>.Failure("permission list is not found");
            }

            return Result<List<Permission>, string>.Success(permission);
        }
    }
}
