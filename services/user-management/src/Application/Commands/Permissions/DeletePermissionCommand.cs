
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class DeletePermissionCommand : IRequest<Result<bool,string>>
    {
        public int Id { get; init; }
        public DeletePermissionCommand(int id)
        {
            Id = id;
        }
    }
}
