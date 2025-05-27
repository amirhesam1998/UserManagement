
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleCommand , Result<List<Role>,string>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetAllRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<List<Role>,string>> Handle(GetAllRoleCommand request , CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            if (roles == null || roles.Count == 0)
            {
                return Result<List<Role>, string>.Failure("Roles list is not found");
            }

            return Result<List<Role>,string>.Success(roles);
        }
    }
}
