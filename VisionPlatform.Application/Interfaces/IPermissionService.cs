namespace VisionPlatform.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(long userId, string permissionName);
    }
}
