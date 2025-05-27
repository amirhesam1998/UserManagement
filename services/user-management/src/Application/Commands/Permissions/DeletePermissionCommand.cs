
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class DeletePermissionCommand : IRequest<Result<bool,string>>
    {
        public Guid Id { get; init; }
        public DeletePermissionCommand(Guid id)
        {
            Id = id;
        }
    }
}
