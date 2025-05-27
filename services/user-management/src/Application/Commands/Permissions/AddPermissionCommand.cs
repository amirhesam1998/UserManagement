
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class AddPermissionCommand : IRequest<Result<Guid , string>>
    {
        public string Name { get; init; }
        public string? PermissionId { get; init; }
        public string Description { get; init; }
        public int Ordering { get; init; }
        public bool Active { get; init; }

        public AddPermissionCommand(string name, string? permissionId, string description , int ordering , bool active)
        {
            Name = name;
            PermissionId = permissionId;
            Description = description;
            Ordering = ordering;
            Active = active;
        }


    }
}
