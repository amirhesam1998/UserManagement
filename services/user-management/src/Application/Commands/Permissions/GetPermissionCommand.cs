
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionCommand : IRequest<Result<Permission , string>>
    {
        public int Id { get; init; }
        public GetPermissionCommand(int id)
        {
            Id = id;
        }
    }
}
