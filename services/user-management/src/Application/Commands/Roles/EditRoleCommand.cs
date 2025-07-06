
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class EditRoleCommand : IRequest<Result<int , string>>
    {
        //public int Id { get; }
        //public string Name { get;  }
        //public string? PermissionId { get;  }
        //public string Description { get; }
        //public int Ordering { get; }
        //public bool Active { get; }
    }
}
