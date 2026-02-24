namespace VisionPlatform.Application.DTOs.VersionTasks
{
    public class CreateVersionTaskDto
    {
        public long VersionId { get; set; }
        public long AzureTaskId { get; set; }
        public string AzureTaskUrl { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;

        public long ClienteId { get; set; }
        public long AreaId { get; set; }

        public string Tipo { get; set; } = string.Empty;
        public string StatusPlanejamento { get; set; } = "Desejavel";

        public long? QaUserId { get; set; }

        public int? OrdemExibicao { get; set; }
    }
}
