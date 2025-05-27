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

        public async Task<bool> ExistSlugAsync(Slug slug)
        {
            return await _dbContext.Roles.AnyAsync(s => s.Slug.Value == slug.Value);
        }

        public async Task<List<Role>?> GetAllRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role> GetBySlugAsync(string slug)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(s => s.Slug.Value == slug);

        }

        public async Task<bool> DeleteRoleAsync(string slug)
        {
            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(u => u.Slug.Value == slug);

            if (role is null)
                return false;

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
