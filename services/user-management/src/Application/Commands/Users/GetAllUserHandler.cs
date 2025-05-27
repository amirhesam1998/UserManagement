using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserCommand, Result<List<User>, string>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<User>, string>> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users == null || users.Count == 0)
                return Result<List<User>, string>.Failure("users is not found");

            return Result<List<User>, string>.Success(users);
        }
    }
}
