
using Application.Commands.Users;
using Application.Interfaces;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand,Result<bool,string>>
    {
        private readonly IRoleRepository _roleRepository;
        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<bool, string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _roleRepository.DeleteRoleAsync(request.Slug);

            if (!isDeleted)
                return Result<bool, string>.Failure("Role is Deleted");

            return Result<bool, string>.Success(true);
        }
    }
}
