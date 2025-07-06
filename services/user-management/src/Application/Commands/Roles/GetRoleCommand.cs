
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class GetRoleCommand : IRequest<Result<Role,string>>
    {
        public int Id { get; }
        public GetRoleCommand(int id)
        {
            Id = id;
        }
    }
}
