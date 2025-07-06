using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;
namespace Application.Commands.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid, string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<Result<Guid, string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetByUsernameAsync(request.UserName , cancellationToken) is not null)
                return Result<Guid, string>.Failure("User already exist");
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return Result<Guid, string>.Failure(emailResult.Error!);
            var usernameResult = UserName.Create(request.UserName);
            if (usernameResult.IsFailure)
                return Result<Guid, string>.Failure(usernameResult.Error!);

            if (request.UserType == UserType.User && request.Role is not null)
                return Result<Guid, string>.Failure("User type 'User' should not have a Role.");
            var user = new User(
                request.FirstName,
                request.LastName,
                emailResult.Value!,
                _passwordHasher.Hash(request.Password),
                usernameResult.Value!,
                request.UserType
            );

            if (request.UserType != UserType.User && request.Role is not null)
            {
                var role = await _roleRepository.GetRoleAsync(request.Role, cancellationToken);
                if (role is not null)
                {
                    user.AssignRole(role);
                }
            }

            await _userRepository.AddAsync(user, cancellationToken);

            return Result<Guid, string>.Success(user.Id);

            //var userExist = await _userRepository.GetByUsernameAsync(request.UserName, cancellationToken);
            //if (userExist != null)
            //{
            //    return Result<Guid, string>.Failure("User already exist");
            //}
            //var emailResult = Email.Create(request.Email);
            //if (!emailResult.IsSuccess)
            //    return Result<Guid, string>.Failure(emailResult.Error!);

            //var usernameResult = UserName.Create(request.UserName);
            //if (!usernameResult.IsSuccess)
            //    return Result<Guid, string>.Failure(usernameResult.Error!);

            //var user = new User(
            //    request.FirstName,
            //    request.LastName,
            //    emailResult.Value!,
            //    _passwordHasher.Hash(request.Password),
            //    usernameResult.Value!,
            //    request.UserType
            //);
            //if(user.Type == UserType.User && request.Role != null)
            //{
            //    return Result<Guid,string>.Failure("User dont have a Roleid");
            //}

            //if (user.Type != UserType.User)
            //{
            //    var role = await _roleRepository.GetRoleAsync(request.Role, cancellationToken);
            //    if (role != null)
            //    {
            //        user.AssignRole(role);
            //    }
            //}

            //await _userRepository.AddAsync(user, cancellationToken);

            //return Result<Guid, string>.Success(user.Id);


        }
    }
}
