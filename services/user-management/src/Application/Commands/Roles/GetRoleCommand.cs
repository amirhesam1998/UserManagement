
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class GetRoleCommand : IRequest<Result<Role,string>>
    {
        public string Slug { get; }
        public GetRoleCommand(string slug)
        {
            Slug = slug;
        }
    }
}
