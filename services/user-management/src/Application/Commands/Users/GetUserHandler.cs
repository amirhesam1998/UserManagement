using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class GetUserHandler : IRequestHandler<GetUserCommand, Result<User, string>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User, string>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user is null)
                return Result<User, string>.Failure("user not found");

            return Result<User, string>.Success(user);
        }
    }
}
