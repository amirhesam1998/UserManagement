using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public class LoginUserCommand : IRequest<Result<string, string>>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
