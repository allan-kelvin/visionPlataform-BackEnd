
using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class VersionRepository: IVersionRepository
    {

        private readonly VisionDbContext _context;

        public VersionRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReleaseVersion>> GetAllAsync()
        {
            return await _context.Versions.ToListAsync();
        }

        public async Task<ReleaseVersion?> GetByIdAsync(long id)
        {
            return await _context.Versions
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ReleaseVersion version)
        {
            await _context.Versions.AddAsync(version);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReleaseVersion version)
        {
            _context.Versions.Update(version);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ReleaseVersion version)
        {
            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();
        }
    }
}
