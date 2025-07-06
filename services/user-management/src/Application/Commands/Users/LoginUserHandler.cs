using Application.Interfaces;
using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<string, string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string, string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

            if (user is null || !_passwordHasher.Verify(request.Password, user.Password))
                return Result<string, string>.Failure("Invalid email or password.");

            var token = _jwtProvider.GenerateToken(user);
            return Result<string, string>.Success(token);
        }
    }
}
