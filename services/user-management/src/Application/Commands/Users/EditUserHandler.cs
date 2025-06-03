
using System.Security;
using Application.Commands.Permissions;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class EditUserHandler : IRequestHandler<EditUserCommand , Result<Guid,string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public EditUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result<Guid, string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return Result<Guid, string>.Failure("User not found.");

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                var result = user.UpdateFirstName(request.FirstName);
                if (result.IsFailure)
                    return Result<Guid, string>.Failure(result.Error!);
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                var result = user.UpdateLastName(request.LastName);
                if (result.IsFailure)
                    return Result<Guid, string>.Failure(result.Error!);
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailResult = Email.Create(request.Email);
                if (emailResult.IsFailure)
                    return Result<Guid, string>.Failure(emailResult.Error!);

                user.UpdateEmail(emailResult.Value!);
            }



            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                var userResult = UserName.Create(request.Username);
                if (userResult.IsFailure)
                    return Result<Guid, string>.Failure(userResult.Error!);

                user.UpdateUsername(userResult.Value!);
            }

            if (request.Type is not null)
            {
                user.UpdateUserType(request.Type.Value);
            }

            if (request.Password != 0 && request.Password == request.Password_confirm)
            {
                var hashed = _passwordHasher.Hash(request.Password.ToString());
                user.UpdatePassword(hashed);
            }

            await _userRepository.UpdateAsync(user);
            return Result<Guid, string>.Success(user.Id);
        }
    }
}
