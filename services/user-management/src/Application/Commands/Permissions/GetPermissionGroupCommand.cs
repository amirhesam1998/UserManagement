using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionGroupCommand : IRequest<Result<List<Permission>? , string>>
    {
        public int Id { get; init; }
        
        public GetPermissionGroupCommand(int id)
        {
            Id = id;
        }
    }
}
