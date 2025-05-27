using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public record LoginUserCommand(string Username, string Password) : IRequest<Result<string, string>>;
}
