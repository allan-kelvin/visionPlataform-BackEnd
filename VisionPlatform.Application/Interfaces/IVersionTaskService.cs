using VisionPlatform.Application.DTOs.VersionTasks;

namespace VisionPlatform.Application.Interfaces
{
    public interface IVersionTaskService
    {
        Task<List<VersionTaskResponseDto>> GetByVersionIdAsync(long versionId);
        Task<long> CreateAsync(CreateVersionTaskDto dto);
        Task DeleteAsync(long id);

        Task UpdateAsync(long id, UpdateVersionTaskDto dto);
        Task MarkMergeAsync(long id, long userId);
        Task AssignQaAsync(long id, long qaUserId);
    }
}
