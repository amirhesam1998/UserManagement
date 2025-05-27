
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class GetAllRoleCommand : IRequest<Result<List<Role>,string>>
    {
    }
}
