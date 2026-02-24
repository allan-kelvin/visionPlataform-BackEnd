namespace VisionPlatform.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public long RoleId { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataAtualizacao { get; set; }

        public Role Role { get; set; }
    }
}
