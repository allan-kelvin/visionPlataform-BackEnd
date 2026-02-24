using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class VersionTaskRepository : IVersionTaskRepository
    {
        private readonly VisionDbContext _context;

        public VersionTaskRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<VersionTask>> GetByVersionIdAsync(long versionId)
        {
            return await _context.VersionTasks
                .Where(x => x.VersionId == versionId)
                .ToListAsync();
        }

        public async Task<VersionTask?> GetByIdAsync(long id)
        {
            return await _context.VersionTasks
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(VersionTask task)
        {
            await _context.VersionTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VersionTask task)
        {
            _context.VersionTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VersionTask task)
        {
            _context.VersionTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
