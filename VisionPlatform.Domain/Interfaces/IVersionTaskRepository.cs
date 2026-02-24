using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Domain.Interfaces
{
    public interface IVersionTaskRepository
    {
        Task<List<VersionTask>> GetByVersionIdAsync(long versionId);
        Task<VersionTask?> GetByIdAsync(long id);
        Task AddAsync(VersionTask task);
        Task UpdateAsync(VersionTask task);
        Task DeleteAsync(VersionTask task);
    }
}
