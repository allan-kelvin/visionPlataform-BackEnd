using VisionPlatform.Application.DTOs.Roles;

namespace VisionPlatform.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllAsync();
    }
}
