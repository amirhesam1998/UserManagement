using Domain.Entities;
using Domain.ValueObject;

namespace Application.Interfaces
{
    public interface IRoleRepository
    {
        Task AddAsync(Role role , CancellationToken cancellationToken);
        Task<bool> ExistSlugAsync(Slug slug);
        Task<List<Role>?> GetAllRolesAsync();
        Task<Role?> GetBySlugAsync(string slug);
        Task<bool> DeleteRoleAsync(string slug);


    }
}
