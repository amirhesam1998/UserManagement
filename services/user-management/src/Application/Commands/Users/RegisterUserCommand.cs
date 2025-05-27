using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<Result<Guid, string>>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string UserName { get; init; }

        public RegisterUserCommand(string firstName, string lastName, string email, string password, string username)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            UserName = username;
        }
    }
}
