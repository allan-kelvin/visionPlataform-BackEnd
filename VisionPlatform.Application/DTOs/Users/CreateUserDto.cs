namespace VisionPlatform.Application.DTOs.Users
{
    public class CreateUserDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public long RoleId { get; set; }
    }
}
