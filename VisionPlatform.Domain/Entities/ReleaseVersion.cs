using VisionPlatform.Domain.Enums;

namespace VisionPlatform.Domain.Entities
{
    public class ReleaseVersion
    {
        public long Id { get; set; }

        public string NumeroVersao { get; set; }

        public VersionStatus StatusVersao { get; set; }

        public DateTime? DataLimiteTarefas { get; set; }

        public DateTime? DataPrevistaLiberacao { get; set; }

        public DateTime? DataLiberacaoReal { get; set; }

        public string? Observacoes { get; set; }

        public long CriadorId { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public ICollection<VersionTask> Tasks { get; set; } = new List<VersionTask>();

        public User Criador { get; set; }
    }
}
