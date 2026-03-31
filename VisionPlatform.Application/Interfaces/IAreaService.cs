using VisionPlatform.Application.DTOs.Area;

namespace VisionPlatform.Application.Interfaces
{
    public interface IAreaService
    {
        Task<List<AreaResponseDto>> GetAllAsync();

        Task<long> CreateAsync(CreateAreaDto dto);

        Task UpdateAsync(long id, CreateAreaDto dto);

        Task<AreaResponseDto?> GetByIdAsync(long id);

        Task DeleteAsync(long id);
    }
}
