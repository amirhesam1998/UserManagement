using Domain.Entities;
using Domain.Enums;
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
        public UserType UserType { get; init; } = UserType.User;
        public int? Role { get; init; }

        //public RegisterUserCommand(string firstName, string lastName, string email, string password, string username, UserType usertype, int rule)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Password = password;
        //    UserName = username;
        //    UserType = usertype;
        //    Role = rule;
        //}
    }
}
