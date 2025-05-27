
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<Result<bool, string>>
    {
        public string Slug { get; }

        public DeleteRoleCommand(string slug)
        {
            Slug = slug;
        }
    }
}
