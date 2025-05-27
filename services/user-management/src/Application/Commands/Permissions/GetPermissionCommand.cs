
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionCommand : IRequest<Result<Permission , string>>
    {
        public Guid Id { get; init; }
        public GetPermissionCommand(Guid id)
        {
            Id = id;
        }
    }
}
