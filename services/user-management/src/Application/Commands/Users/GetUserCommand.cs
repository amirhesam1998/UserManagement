using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class GetUserCommand : IRequest<Result<User, string>>
    {
        public string Username { get; }
        public GetUserCommand(string username)
        {
            Username = username;
        }
    }

}
