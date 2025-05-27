
using Application.Commands.Users;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class GetRoleHandler : IRequestHandler<GetRoleCommand , Result<Role,string>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetRoleHandler(IRoleRepository userRepository)
        {
            _roleRepository = userRepository;
        }

        public async Task<Result<Role, string>> Handle(GetRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetBySlugAsync(request.Slug);
            if (role is null)
                return Result<Role, string>.Failure("role not found");

            return Result<Role, string>.Success(role);
        }
    }
}
