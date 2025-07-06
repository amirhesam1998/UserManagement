
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AddPermissionCommand : IRequest<Result<int , string>>
    {
        public string Name { get; init; }
        public string? PermissionId { get; init; }
        public string Description { get; init; }
        public bool Active { get; init; }

        


    }
}
