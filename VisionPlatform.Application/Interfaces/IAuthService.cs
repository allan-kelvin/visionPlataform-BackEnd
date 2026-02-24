using VisionPlatform.Application.DTOs.Auth;

namespace VisionPlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
