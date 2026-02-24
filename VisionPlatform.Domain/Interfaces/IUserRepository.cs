using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(long id);

        Task AddAsync(User user);

        Task<List<User>> GetAllAsync();
        Task UpdateAsync(User user);

    }
}
