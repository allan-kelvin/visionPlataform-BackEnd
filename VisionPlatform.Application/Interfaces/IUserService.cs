using VisionPlatform.Application.DTOs.Users;

namespace VisionPlatform.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(long id, UpdateUserDto dto);
        Task DeleteAsync(long id);
    }
}
