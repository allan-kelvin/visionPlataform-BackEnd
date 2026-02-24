namespace VisionPlatform.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
