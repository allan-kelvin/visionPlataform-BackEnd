using VisionPlatform.Domain.Enums;

namespace VisionPlatform.Domain.Entities
{
    public class TestEvidence
    {
        public long Id { get; set; }

        public long VersionTaskId { get; set; }

        public long QaUserId { get; set; }

        public EvidenceType Tipo { get; set; } // Imagem | VideoLink

        public string? NomeArquivo { get; set; }

        public string? CaminhoArquivo { get; set; }

        public string? UrlVideo { get; set; }

        public string? Observacao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public VersionTask VersionTask { get; set; }

        public User QaUser { get; set; }
    }
}
