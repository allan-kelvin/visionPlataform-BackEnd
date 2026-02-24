using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Domain.Interfaces
{
    public interface IVersionRepository
    {
        Task<List<ReleaseVersion>> GetAllAsync();
        Task<ReleaseVersion?> GetByIdAsync(long id);
        Task AddAsync(ReleaseVersion version);
        Task UpdateAsync(ReleaseVersion version);
        Task DeleteAsync(ReleaseVersion version);
    }
}
