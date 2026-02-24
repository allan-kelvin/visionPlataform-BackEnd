namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class UpdateVersionTaskDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string StatusPlanejamento { get; set; } = string.Empty;
        public int? OrdemExibicao { get; set; }
    }
}
