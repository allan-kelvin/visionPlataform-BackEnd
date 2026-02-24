using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Data;

namespace VisionPlatform.Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly VisionDbContext _context;

        public AreaRepository(VisionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAllAsync()
        {
            return await _context.Areas
                .OrderBy(a => a.Descricao)
                .ToListAsync();
        }

        public async Task<Area?> GetByIdAsync(long id)
        {
            return await _context.Areas
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Area area)
        {
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Area area)
        {
            _context.Areas.Update(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Area area)
        {
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
        }
    }
}
