namespace VisionPlatform.Application.DTOs.Versions
{
    public interface IVersionService
    {
        Task<List<VersionResponseDto>> GetAllAsync();
        Task<VersionResponseDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateVersionDto dto, long userId);
        Task UpdateAsync(long id, UpdateVersionDto dto);
        Task DeleteAsync(long id);
        Task ReleaseVersionAsync(long versionId);
    }
}
