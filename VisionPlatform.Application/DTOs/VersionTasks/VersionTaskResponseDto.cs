namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class VersionTaskResponseDto
    {
        public long Id { get; set; }
        public long VersionId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string StatusPlanejamento { get; set; } = string.Empty;
        public bool MergeRealizado { get; set; }
        public bool PossuiScript { get; set; }
        public bool PossuiTagVersao { get; set; }
    }
}
