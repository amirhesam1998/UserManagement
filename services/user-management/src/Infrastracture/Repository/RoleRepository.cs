using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserDbContext _dbContext;

        public RoleRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Role>?> GetAllRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<bool> ExistRoleAsync(string title, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.AnyAsync(s => s.Title == title, cancellationToken);
        }

        public async Task<Role> GetRoleAsync(int? id , CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<bool> DeleteRoleAsync(int id , CancellationToken cancellationToken)
        {
            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(u => u.Id == id);

            if (role is null)
                return false;

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
