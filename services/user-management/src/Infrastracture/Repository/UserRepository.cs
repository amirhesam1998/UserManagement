
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastracture.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }


        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username.Value == username);
        }

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username.Value == username);

            if (user is null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsByEmailAsync(Email email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(UserName username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }


    }
}
