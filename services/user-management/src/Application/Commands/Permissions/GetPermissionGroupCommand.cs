using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class GetPermissionGroupCommand : IRequest<Result<List<Permission>? , string>>
    {
        public Guid Id { get; init; }
        
        public GetPermissionGroupCommand(Guid id)
        {
            Id = id;
        }
    }
}
