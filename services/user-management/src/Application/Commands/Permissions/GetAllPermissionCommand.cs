
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetAllPermissionCommand : IRequest<Result<List<Permission>,string>>
    {
    }
}
