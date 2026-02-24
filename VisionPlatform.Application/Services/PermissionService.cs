using VisionPlatform.Application.Interfaces;
using VisionPlatform.Domain.Interfaces;

namespace VisionPlatform.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IUserRepository _userRepository;

        public PermissionService(IPermissionRepository repository,
                                 IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<bool> HasPermissionAsync(long userId, string permissionName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            // 🔹 Primeiro verifica se há permissão customizada
            var userPermissions = await _repository.GetUserPermissionsAsync(userId);

            var custom = userPermissions
                .FirstOrDefault(p => p.Permission.Nome == permissionName);

            if (custom != null)
                return custom.IsAllowed;

            // 🔹 Se não houver customizada, usa RolePermission
            var rolePermissions = await _repository
                .GetRolePermissionsAsync(user.RoleId);

            return rolePermissions
                .Any(rp => rp.Permission.Nome == permissionName);
        }
    }
}
