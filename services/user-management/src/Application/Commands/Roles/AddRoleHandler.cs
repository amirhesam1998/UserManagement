using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand,Result<int,string>>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<int,string>> Handle(AddRoleCommand request , CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.ExistRoleAsync(request.Title, cancellationToken);
            
            if (roleExist == true)
            {
                return Result<int,string>.Failure("Role already exists with this ID.");
            }

            var role = new Role(
                request.Title,
                request.Description
                );
            await _roleRepository.AddAsync(role, cancellationToken);
            return Result<int,string>.Success(role.Id);
        }
    }
}
