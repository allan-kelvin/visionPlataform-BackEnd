using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Domain.Interfaces
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAsync();
        Task<Area?> GetByIdAsync(long id);
        Task AddAsync(Area area);
        Task UpdateAsync(Area area);
        Task DeleteAsync(Area area);
    }
}
