using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPermissionRepository
    {
        Task AddAsync(Permission permission, CancellationToken cancellationToken);
        Task<Permission?> GetByIdAsync(int id , CancellationToken cancellationToken);
        Task<List<Permission>?> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
        Task<List<Permission>> GetAllPermissionAsync();
        Task<bool> DeletePermissionAsync(int id);
        Task UpdateAsync(Permission permission, CancellationToken cancellationToken);

        Task<List<Permission>?> GetPermissionGroupAsync(Permission permission);

    }
}
