using VisionPlatform.Domain.Enums;

namespace VisionPlatform.Application.DTOs.Versions
{
    public class CreateVersionDto
    {
        public string NumeroVersao { get; set; } = string.Empty;
        public VersionStatus StatusVersao { get; set; } = VersionStatus.Planejamento;
        public DateTime? DataLimiteTarefas { get; set; }
        public DateTime? DataPrevistaLiberacao { get; set; }
        public string? Observacoes { get; set; }
    }
}
