using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Shared.ResultManagement;
using static Shared.ResultManagement.Errors;

namespace Infrastracture.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UserDbContext _dbContext;

        public PermissionRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Permission permission, CancellationToken cancellationToken)
        {
            _dbContext.Permissions.Add(permission);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Permissions.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<Permission>?> GetAllPermissionAsync()
        {
            return await _dbContext.Permissions.ToListAsync();
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _dbContext.Permissions
                .FirstOrDefaultAsync(p => p.Id == id);
            if (permission is null)
                return false;
            if (permission.PermissionId is null)
            {
                var children = await _dbContext.Permissions
                    .Where(p => p.PermissionId == id)
                    .ToListAsync();
                _dbContext.Permissions.RemoveRange(children);

            }

            _dbContext.Permissions.Remove(permission);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken)
        {
            _dbContext.Permissions.Update(permission);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Permission?>> GetPermissionGroupAsync(Permission permission)
        {
            return await _dbContext.Permissions.Where(p => p.PermissionId == permission.Id).ToListAsync();
        }

        public async Task<List<Permission>?> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            return await _dbContext.Permissions
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
