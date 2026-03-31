using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VisionDbContext _context;

        public RoleRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
