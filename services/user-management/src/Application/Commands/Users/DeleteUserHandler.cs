
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool, string>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool, string>> Handle(DeleteUserCommand request , CancellationToken cancellationToken)
        {
            var isDeleted = await _userRepository.DeleteUserAsync(request.Username);

            if (!isDeleted)
                return Result<bool, string>.Failure("کاربر حذف نشد یا وجود ندارد.");

            return Result<bool, string>.Success(true);
        }
    }
}
