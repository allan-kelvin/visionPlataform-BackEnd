
namespace VisionPlatform.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public string Nome {  get; set; } = string.Empty;
        public long RoleId { get; set; }
        public bool Ativo { get; set; }
    }
}
