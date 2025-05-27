using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPermissionRepository
    {
        Task AddAsync(Permission permission, CancellationToken cancellationToken);
        Task<Permission?> GetByIdAsync(Guid id , CancellationToken cancellationToken);
        Task<List<Permission>> GetAllPermissionAsync();
        Task<bool> DeletePermissionAsync(Guid id);
        Task UpdateAsync(Permission permission, CancellationToken cancellationToken);

        Task<List<Permission>?> GetPermissionGroupAsync(Permission permission);

    }
}
