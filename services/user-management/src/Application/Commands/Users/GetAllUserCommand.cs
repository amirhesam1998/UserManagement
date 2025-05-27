using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class GetAllUserCommand : IRequest<Result<List<User>, string>>
    {
    }
}
