
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Result<bool, string>>
    {
        public string Username { get;}

        public DeleteUserCommand(string username)
        {
            Username = username;
        }
    }
}
