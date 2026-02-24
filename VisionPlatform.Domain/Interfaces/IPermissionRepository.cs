using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<RolePermission>> GetRolePermissionsAsync(long roleId);
        Task<List<UserPermission>> GetUserPermissionsAsync(long userId);
    }
}
