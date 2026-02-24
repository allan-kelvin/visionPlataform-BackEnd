using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
