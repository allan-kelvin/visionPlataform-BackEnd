namespace VisionPlatform.Application.DTOs.Versions
{
    public class VersionResponseDto
    {
        public long Id { get; set; }
        public string NumeroVersao { get; set; } = string.Empty;
        public string StatusVersao { get; set; } = string.Empty;
        public DateTime? DataLimiteTarefas { get; set; }
        public DateTime? DataPrevistaLiberacao { get; set; }
        public DateTime? DataLiberacaoReal { get; set; }
        public string? Observacoes { get; set; }
    }
}
