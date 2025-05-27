using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand,Result<Guid,string>>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<Guid,string>> Handle(AddRoleCommand request , CancellationToken cancellationToken)
        {
            var slugResult = Slug.Create(string.IsNullOrWhiteSpace(request.Slug) ? request.Title : request.Slug);
            if (!slugResult.IsSuccess)
                return Result<Guid, string>.Failure(slugResult.Error!);

            if (await _roleRepository.ExistSlugAsync(slugResult.Value))
                return Result<Guid, string>.Failure("Slug is already exist");

            var role = new Role(
                request.Title,
                slugResult.Value!,
                request.Description
                );
            await _roleRepository.AddAsync(role, cancellationToken);
            return Result<Guid,string>.Success(role.Id);
        }
    }
}
