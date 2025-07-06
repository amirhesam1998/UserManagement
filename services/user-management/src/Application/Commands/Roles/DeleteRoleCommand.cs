
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<Result<bool, string>>
    {
        public int Id { get; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}
