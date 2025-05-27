using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid, string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid, string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (!emailResult.IsSuccess)
                return Result<Guid, string>.Failure(emailResult.Error!);

            var usernameResult = UserName.Create(request.UserName);
            if (!usernameResult.IsSuccess)
                return Result<Guid, string>.Failure(usernameResult.Error!);

            if (await _userRepository.ExistsByEmailAsync(emailResult.Value!))
                return Result<Guid, string>.Failure("Email is already exist");

            if (await _userRepository.ExistsByUsernameAsync(usernameResult.Value!))
                return Result<Guid, string>.Failure("Username is already exist");

            var user = new User(
                request.FirstName,
                request.LastName,
                emailResult.Value!,
                _passwordHasher.Hash(request.Password),
                usernameResult.Value!
            );

            await _userRepository.AddAsync(user, cancellationToken);

            return Result<Guid, string>.Success(user.Id);
        }
    }
}
