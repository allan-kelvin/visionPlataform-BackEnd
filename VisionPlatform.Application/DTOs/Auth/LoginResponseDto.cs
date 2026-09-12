namespace VisionPlatform.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public long UserId { get; set; }

        public string Nome { get; set; }
    }
}
