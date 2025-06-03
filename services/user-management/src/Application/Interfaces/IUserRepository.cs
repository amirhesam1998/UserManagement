using Domain.Entities;
using Domain.ValueObject;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>?> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string username);

        Task<bool> ExistsByEmailAsync(Email email);
        Task<bool> ExistsByUsernameAsync(UserName username);
        Task UpdateAsync(User user);

    }
}
