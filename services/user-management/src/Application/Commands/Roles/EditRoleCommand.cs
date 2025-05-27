
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class EditRoleCommand : IRequest<Result<Guid , string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? PermissionId { get; set; }
        public string Description { get; set; }
        public int Ordering { get; set; }
        public bool Active { get; set; }
    }
}
