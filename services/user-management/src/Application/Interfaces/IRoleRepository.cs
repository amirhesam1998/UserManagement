using Domain.Entities;
using Domain.ValueObject;

namespace Application.Interfaces
{
    public interface IRoleRepository
    {
        Task AddAsync(Role role , CancellationToken cancellationToken);
        Task<bool> ExistRoleAsync(string title, CancellationToken cancellationToken);
        Task<List<Role>?> GetAllRolesAsync();
        Task<Role?> GetRoleAsync(int? id , CancellationToken cancellationToken);
        Task<bool> DeleteRoleAsync(int id , CancellationToken cancellationToken);
        Task UpdateAsync(Role role, CancellationToken cancellationToken);



    }
}
