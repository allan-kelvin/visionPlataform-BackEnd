using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly VisionDbContext _context;

        public PermissionRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<RolePermission>> GetRolePermissionsAsync(long roleId)
        {
            return await _context.Set<RolePermission>()
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<List<UserPermission>> GetUserPermissionsAsync(long userId)
        {
            return await _context.Set<UserPermission>()
                .Where(up => up.UserId == userId)
                .Include(up => up.Permission)
                .ToListAsync();
        }
    }
}
