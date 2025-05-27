
using Application.Interfaces;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand , Result<bool,string>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public DeletePermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<bool, string>> Handle(DeletePermissionCommand request,CancellationToken cancellationToken)
        {
            var isDeleted = await _permissionRepository.DeletePermissionAsync(request.Id);

            if (!isDeleted)
                return Result<bool, string>.Failure("Permission is Deleted");

            return Result<bool, string>.Success(true);
        }
    }
}
